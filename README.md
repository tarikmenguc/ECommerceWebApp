

```markdown
# ECommerceWebApp

## 📖 Overview

Seller Automation System is a **desktop application** built using **WinForms** that provides sellers with a streamlined interface to manage their products, orders, and overall inventory effectively. This project focuses on offering essential automation tools for sellers to operate their businesses more efficiently, with plans to expand into a **web-based platform** and **WPF application** for enhanced user experiences in the future.

The system ensures sellers can maintain a complete overview of their products and orders while leveraging a robust database backend to store and process data.

---

## 🚀 Features

### **Product Management**
- Add, edit, and delete products.
- Manage product categories and brands.
- Monitor stock levels.
- Upload and display product images.
- Filter products by category, brand, and user.

### **Order Management**
- Place new orders and track order statuses.
- Manage shipping addresses and shipping statuses.
- View detailed order items and total order costs.
- Filter orders by status (Pending, Processing, Shipped, Delivered, Canceled).

### **User Management**
- Dynamic user-based data filtering.
- User-specific access control to ensure sellers only manage their own data.
- Support for multiple roles (Admin, Seller).

### **Invoice and Payment Management**
- Generate and view invoices for completed orders.
- Track payment statuses (Pending, Completed, Failed, Refunded).
- Support multiple payment methods (Credit Card, Cash on Delivery, etc.).

### **Modern User Interface**
- Enhanced with **MaterialSkin** and **Siticone UI Framework**.
- Clean, intuitive, and responsive design for better usability.
- Customizable themes for a personalized experience.

### **Reporting and Analytics**
- Generate sales and revenue reports.
- Visualize data with **ScottPlot**.
- Export reports for business analysis.

### **Notifications**
- Notify users of critical updates, such as low stock or pending payments.
- Manage notifications and track read status.

### **Upcoming Features**
- Integration with **AWS S3** for file storage.
- Migration to **AWS RDS** for database hosting.
- Web-based application using **ASP.NET Core**.
- Advanced authentication and authorization.
- Real-time data synchronization.

---

## 📸 Application Screenshots

### Product Management Interface
![Product Management](images/product-management.png "Product Management Interface")

### Order Management Interface
![Order Management](images/order-management.png "Order Management Interface")

---

## 🎥 Demo Video

Watch the project introduction video on [YouTube](https://www.youtube.com/watch?v=video_id).

Alternatively, use the embedded video below:

```html
<iframe width="560" height="315" src="https://www.youtube.com/embed/video_id" frameborder="0" allowfullscreen></iframe>
```

---

## 🛠️ Technologies Used

### **Backend Technologies**
- **ASP.NET Core**: Web and API development.
- **Entity Framework Core**: ORM for database operations.
- **ADO.NET**: Raw SQL queries and database communication.

### **Frontend Technologies**
- **WinForms**: Desktop user interface.
- **MaterialSkin & Siticone UI Framework**: Modern design components.
- **ScottPlot**: Advanced charting library for reports.

### **Database**
- **MSSQL** (Local for initial development).
- Planned migration to **AWS RDS**.

### **Other Tools**
- **Docker**: Containerization for deployment.
- **AWS S3**: Cloud storage for images and files (future).

---

## 📂 Project Structure

```
SellerAutomationSystem/
|-- Data/
|   |-- AppDbContext.cs        # Database context and configurations
|
|-- Models/
|   |-- User.cs                # User model with properties like UserName, Email
|   |-- Product.cs             # Product model with details like price, stock
|   |-- Order.cs               # Order model with relationships to OrderItems
|   |-- ... (Other models)
|
|-- Repositories/
|   |-- ProductRepository.cs   # Product-specific database operations
|   |-- OrderRepository.cs     # Order-related database operations
|
|-- Services/
|   |-- ProductService.cs      # Business logic for products
|   |-- OrderService.cs        # Business logic for orders
|
|-- Forms/
|   |-- ProductForm.cs         # UI for managing products
|   |-- OrderForm.cs           # UI for managing orders
|
|-- Resources/
|   |-- Images/                # Local images (temporary, before AWS S3 integration)
|   |-- CSS/                   # Styling for forms and reports
```

---

## 🚀 Getting Started

### Prerequisites
- **Visual Studio 2022** or later.
- **.NET 6.0 SDK** or higher.
- **MSSQL Server**.

### Setup Steps

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/seller-automation-system.git
   cd seller-automation-system
   ```

2. **Install Dependencies**:
   Open the solution file (`SellerAutomationSystem.sln`) in Visual Studio and restore NuGet packages.

3. **Configure the Database**:
   - Update the `appsettings.json` file with your database connection string:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=.\\\\SQLEXPRESS;Database=SellerAutomation;Trusted_Connection=True;TrustServerCertificate=True;"
       }
     }
     ```
   - Apply migrations to set up the database:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

4. **Run the Application**:
   - Press `F5` or use the \"Run\" button in Visual Studio to start the project.

---

## 🌟 Future Enhancements

- **WPF Desktop Application**: A modern, feature-rich alternative to WinForms.
- **Web Application**: A comprehensive platform for customers and sellers.
- **Mobile Application**: Native apps for managing the platform on-the-go.
- **Multi-language Support**: Localized UI for global users.
- **AI-powered Recommendations**: Suggest products based on user behavior.

---

## 🤝 Contributing

We welcome contributions! To contribute:
1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-name`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to your branch (`git push origin feature-name`).
5. Open a pull request.

---

## 📧 Contact

For inquiries or support, please email **your-email@example.com**.

---

## 📜 License

This project is licensed under the **MIT License**. See the `LICENSE` file for more details.
```

Görsel ve video ekleme kısmı dahil edilmiş ve örneklerle gösterilmiştir. İsterseniz direkt projenize entegre edebilirsiniz.
