# ECart Application Readme

Welcome to the ECart application! This application provides a simple interface for managing items, shopping, and creating orders. Below you will find instructions on how to set up and use the application.

**Setup**
1.Clone the Repository:
Clone the code repository to your local machine using the following command:
git clone <repository-url>

2.Database Setup:
The application uses a MySQL database named ECartDb.
Ensure you have MySQL installed and running on your system.
Execute the SQL scripts provided in the database-scripts folder to create the necessary tables: Categories, Items, Orders, and OrderDetails.

3.Launch the Application:
Open the application in your preferred code editor.
Launch the application using a local development server(preferably Google Chrome).

**Usage
Items Menu**
> The Items menu allows you to add items to the database.
> Navigate to the Items menu to add new items by providing details such as item name, category, price, image path etc.

**Shopping Menu**
> The Shopping menu displays a list of items available for shopping.
> Browse through the list of items and add them to your cart by clicking the "Add to Cart" button.

**Cart**
> The Cart displays all the products you have selected for purchase.
> Review the items in your cart and proceed to save your order by clicking the "Save Order" button.
> Upon clicking "Save", the order is created and added to the Orders table in the database.
> "Back To Shopping" navigates back to shopping page from cart.

**Additional Information**
> **Database Schema:**
The application's database (ECartDB) consists of four tables:
  **Categories**: Stores information about different item categories.
  **Items**: Contains details of individual items available for purchase.
  **Orders**: Records information about customer orders.
  **OrderDetails**: Stores the details of items included in each order.

>**Running the Application:**
Make sure you have a web browser installed (preferably Google Chrome).
Launch the application from VS.

Thank you for using the ECart application! 
