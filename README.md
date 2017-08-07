# Avidyne Flight Data Splitter

This utility application will take an input .CSV file from an Avidyne IFD540 or IFD440 flight log export and break the data into individual files based on individual cycles of the unit.  For those looking to analyze flight information using third-party tools such as CloudAhoy or similar that need the files individually created this can help save a lot of time.

##Usage 

1. Rename your downloaded file to "AvidyneLog.csv"
2. Download the "Release" of the application from GitHub
3. Unzip the release to a location of your choosing
4. Copy the AvidyneLog.csv file to the same folder as the unzipped items from #3
5. Run the .exe, and the files will be created

##Known Limitations

- System will provide output for "updates" or other cycles that did not result in flight, these need to be currently manullay ignored
- System does not validate integrity of flight data.  Avidyne at times will put in "bad" data if the GPS lock takes a bit.  Future versions should help clean this data

##Questions

If you have any questions about this utility, please post an issue here on GitHub, or email me directly at msellers@iowacomputergurus.com
