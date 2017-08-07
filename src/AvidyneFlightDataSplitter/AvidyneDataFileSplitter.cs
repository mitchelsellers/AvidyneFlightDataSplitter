using System;
using System.Collections.Generic;
using System.IO;

namespace MitchelSellers.AvidyneFlightDataSplitter
{
    public class AvidyneDataFileSplitter
    {
        private const string FileHeader =
            "Systime,Date,Time,Heading (°M),PressureAltitude (ft),IndicatedAirspeed (kts),TrueAirspeed (kts),VerticalSpeed (ft/min),GPSLatitude,GPSLongitude,Roll (deg),Pitch (deg),Long. Accel (G),Lat. Accel (G),Norm. Accel (G),Roll Rate (deg/sec),Pitch Rate (deg/sec),Yaw Rate (deg/sec),Hdg Rate (deg/sec),AHRS Seq,AHRS Flags,Splat Flags";
        private const string FileStartIdentifier = "**** POWER ON ****";
        private string fileLocation;

        public AvidyneDataFileSplitter(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Could not find file '{filePath}'.  Please ensure it exists");
            using (var reader = new StreamReader(filePath))
            {
                var headerLine = reader.ReadLine();
                if (!headerLine.StartsWith("Systime,Date,Time,Heading ("))
                    throw new ApplicationException($"Provided data file does not contain the expected format");
            }

            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            fileLocation = filePath;
        }

        public void ProcessDataFile()
        {
            using (var reader = new StreamReader(fileLocation))
            {
                //Read out the header
                reader.ReadLine();

                //Read in the first line of the data, we know it is a power on
                var currentFileLines = new List<string>();
                var powerOnLine = reader.ReadLine();
                var toSaveFileName = GetDesiredFileName(powerOnLine);
                currentFileLines.Add(powerOnLine);

                while (reader.Peek() != -1)
                {
                    var currentLine = reader.ReadLine();
                    if (currentLine.Contains(FileStartIdentifier))
                    {
                        WriteFile(toSaveFileName, currentFileLines);

                        //Process the new to save name
                        toSaveFileName = GetDesiredFileName(currentLine);
                        currentFileLines = new List<string>();
                        currentFileLines.Add(currentLine);

                        //Let the user know
                        Console.WriteLine($"Processing flight from {toSaveFileName}");
                    }
                    else
                    {
                        currentFileLines.Add(currentLine);
                    }
                }

                //Write out the last file
                WriteFile(toSaveFileName, currentFileLines);
            }
        }

        private static void WriteFile(string toSaveFileName, List<string> currentFileLines)
        {
            //Split out the file
            using (var newFileWriter = new StreamWriter($"Output/{toSaveFileName}"))
            {
                newFileWriter.WriteLine(FileHeader);
                foreach (var line in currentFileLines)
                {
                    newFileWriter.WriteLine(line);
                }
            }
        }

        private string GetDesiredFileName(string powerOnData)
        {
            var parts = powerOnData.Split(',');
            var date = parts[1];
            var time = parts[2].Replace(":", "-");
            return $"{date}-{time}-Flight.csv";
        }
    }
}