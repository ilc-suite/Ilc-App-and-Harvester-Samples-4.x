# ILC App and Harvester Samples

## Sample Harvester: Custom InformationObjects and collect Details

The AdventureWorks harvester of step 2 is extended to use custom InformationObjects. 
The custom types are BikeProduct and ProductPhoto.
The harvester can now collect informations of products and add DetailLinks for each product.

With the DetailLink a client can request detailed information which only will be available when
an information is viewed in the details view. 
In this sample the Product when a bike product is viewed an details are requested
the harvester reads the image bytes from the database and returns them.
