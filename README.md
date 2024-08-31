# Prods (Order Management System):

The Prods Order Management System is a robust web application developed using ASP.NET Core, 
with a SQL Server database managed through SQL Server Management Studio (SSMS). The system is designed to
streamline the process of managing product orders by integrating essential features like email notifications through MailJet.
This application is tailored to cater to both administrators and employees, each with distinct roles and privileges, making it a versatile tool for order management.

Key Features and Concepts

🔸Dual Login System:

The application supports two different types of users: Admin and Employee, each with specific privileges.
        🔹Admin: Has full access to the application, including management of products, orders, and other administrative tasks. Admins can perform CRUD (Create, Read, Update, Delete) operations on all available entities within the system.
        🔹Employee: Focuses on managing orders, assisting users with order placements, and ensuring smooth order processing. Employees have restricted access compared to Admins, tailored to their role-specific tasks.

🔸Order Management for Employees:
Employees are primarily responsible for handling customer orders. They can guide users through the product selection and ordering process.
The system provides employees with the tools to manage the entire lifecycle of an order, including order creation, tracking, updating, and resolving any customer inquiries related to orders.



🔸Comprehensive Admin Capabilities:
Admins can manage the entire catalog of products and orders. They have the ability to add new products, update existing product details, remove products, and oversee the inventory.
The Admin panel includes advanced functionalities for order oversight, allowing admins to monitor, update, and manage all orders placed within the system.

🔸Automated Email Notifications:
The system utilizes MailJet for email integration, providing automated email notifications to users. After a successful order placement and payment, users receive a confirmation email detailing their order.
The email system is designed to enhance communication with customers, keeping them informed about their order status and any updates in a timely manner.

