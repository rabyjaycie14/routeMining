# Route Mining
Software Architecture and Design Patterns | University of Michigan-Dearborn | Dearborn, MI | 2019 </br>
</BR>
<b>DISCLAIMER:</b> All verbiage in this document (unless otherwise stated) was provided as project specifications in CIS 476 by Professor Xu at University of Michigan- Dearborn.

# Objective
  In this project, you are required to design a data driven direct mail planning software “RouteMining” that will help marketing team to     analyze specific carrier routes when mapped to house address. 
  The RouteMining can be standalone or web-based software with the following functions/features.
  
  1) Input is a list of addresses in Excel format containing street number, street name, optional apartment number, city, state, zip.</br>
  
  2) RouteMining should be able to connect to google, usps or any web or API of your choice to validate and correct the addres
      automatically. <br/>
      
  3) RouteMining should be able to use https://eddm.usps.com/eddm/customer/routeSearch.action or any web API of
     your choice to find an address’s carrier route. </br>
     
  4) RouteMining should provide a GUI to display two reports and allow a user to export the report in Excel format.</br>
     a) Report one: a list of addresses plus carrier route
     b) Report two: a list of route plus number of addresses in each route.
     
 # Additional Considerations
  1) Provide a detailed class diagram, mapping pattern classes to actual application classes
  2) A minimum of three design patterns must be used to solve the problem
  3) Developed code must be throroughly commented and synchronized with the model
