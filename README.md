# Route Mining
Software Architecture and Design Patterns | University of Michigan-Dearborn | Dearborn, MI | 2019 </br>
</BR>

# Objective
  RouteMining is a standalone data driven direct mail planning software that will help marketing teams to analyze specific carrier routes     when mapped to a house address, with the following functions/features:</br> 
  
  1) Input is a street number, street name, city, state and zip.</br>
  
  2) RouteMining connects to SmartyStreets API to validate an address and return that addresses route carrier number. <br/>

  4) RouteMining provides a GUI to display two reports and allow a user to export the report as a .txt file.</br>
     a) Report one: input address validation, for example:
     
          New file created: 4/5/2019 8:59:09 PM
          Address Validation provided by SmartyStreets API
          https://smartystreets.com/products/single-address
          Address: XXXX Jones Street New York City, New York 12345
          
     b) Report two: carrier route for the address, for example:
     
          New file created: 4/5/2019 8:59:10 PM
          Carrier Route provided by SmartyStreets API
          https://smartystreets.com/products/single-address
          Carrier Route for XXXX Jones Street New York City, New York 12345 is C001 (provided by SmartyStreets API)
