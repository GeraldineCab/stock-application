## Welcome
Hello, welcome to the StockApplication!

This is a single chatroom application where you and more people can chat with each other and also get the current stock close price

Follow this instructions to make the most of this application:

**System requirements**
- Target framework: .Net 5.0
- Docker Engine 20.10.12
- Docker Desktop 4.5.1

**Steps to enter the chatroom**

1. Download the source code from this repository
2. Run *add-migration InitialMigration* command in the VS PackageManagerConsole to get the database building

    - Besides creating the database instance, this command seeds users data with the following credentials, use them to log in the application<br><br>
    
    
    | Email | Password |
    | --- | ----------- |
    | josh@mail.com | J123osh |
    | mary@mail.com | M123ary |
      
3. Set *StockApplication.Web* as the startup project by right clicking over the project
4. Build the application to make sure it compiles successfully
5. Run the application and log in as any of the given users
6. Open a new ingcognito browser window and log in as the other user
7. Start chatting
8. For getting stock information, send the stock code by using */stock={your_stock_code}*
9. To abandon the chatroom you just have to log out
___

### API Availability

An API is also available to retrieve stock information :) Follow this instructions to access:

1. Set *StockApplication.Bot* as the startup project by right clicking over the project
4. Build the application to make sure it compiles successfully
5. Run the application, no authentication is necessary, this will open the Swagger UI
8. Look for the *api/stocks* endpoint and send only the stock code, e.g. AAPL.US (case insensitive)

    - If an invalid stock code is sent or it doesn't exist a bad request response will be get


That's pretty much all, hope both resources are useful :)
