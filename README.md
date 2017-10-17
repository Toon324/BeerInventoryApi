# BeerInventoryApi

A WebAPI that provides a highly-available API for the management of beer in various locations for the Beer Inventory App.

Data is stored in Azure Tables for quick and cheap access.

The API handles the relationship between UPC's and the beer(s) they represent, and maintains data that relates to the beer, such as ABV and user-friendly descriptions of the taste.

The API additionally offers an endpoint for Api.AI (now known as DialogFlow) to expose data via voice assistants such as Google Assistant with the theming of the oft-drunk character "Archer"

## Roadmap
- [X] Azure Table Storage integration
- [X] UPC Lookups and Relations
- [X] Beer Detail Lookups via BreweryDB
- [X] API.AI Implementation
- [ ] End to End Authentication and Encryption
- [ ] Dockerize for easy load-balancing
