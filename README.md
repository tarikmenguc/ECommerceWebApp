

```markdown
# 🌐 E-Commerce Web Application

## 📖 Overview

This repository houses the **E-Commerce Web Application**, designed as a complete platform for **users** to browse and purchase products and for **sellers** to manage their inventories and orders. The web application provides an intuitive and dynamic interface powered by **ASP.NET Core** and integrates with the **Seller Automation System**, a WinForms desktop application.

### Highlights:
- **For Users**: Seamlessly browse products, manage carts, track orders, and save favorites.
- **For Sellers**: Efficiently manage products, orders, and analytics through a dedicated dashboard.
- **Scalable Infrastructure**: Planned integration with **AWS S3** for file storage and **AWS RDS** for databases.

---

## 🌟 Features

### **For Users**
- **Product Catalog**: Search, filter, and view detailed product pages with reviews and ratings.
- **Shopping Cart**: Add/remove items and calculate totals dynamically.
- **Order Management**: Track current orders, view order history, and handle returns.
- **Wishlist**: Save favorite products for later.
- **Account Management**: Register, log in, and manage profiles securely.

### **For Sellers**
- **Product Management**: Add, edit, delete, and organize products.
- **Order Management**: Monitor orders, update statuses, and generate invoices.
- **Sales Analytics**: Access visualized reports of sales and revenue.

### **Additional Features**
- **Secure Payments**: Multi-gateway support with payment status tracking.
- **Responsive Design**: Optimized for desktop, tablet, and mobile users.
- **Real-Time Updates**: Instant notifications for status changes and reviews.
- **Cloud Integration**: AWS S3 for media storage and AWS RDS for scalability (planned).

---

## 📂 Project Structure

```plaintext
ECommerceWeb/
├── Controllers/
│   ├── HomeController.cs          # Handles user-facing pages
│   ├── ProductController.cs       # Manages product-related actions
│   ├── OrderController.cs         # Manages orders and tracking
│   ├── AdminController.cs         # Admin dashboard for sellers
│
├── Models/
│   ├── User.cs                    # User details and authentication
│   ├── Product.cs                 # Product data with stock and pricing
│   ├── Order.cs                   # Order and order items with relationships
│   ├── Invoice.cs                 # Invoice details for completed orders
│   ├── Wishlist.cs                # User's saved products
│
├── Views/
│   ├── Shared/                    # Layouts and partial views
│   ├── Home/                      # User-facing pages
│   ├── Admin/                     # Seller dashboard views
│   ├── Cart/                      # Shopping cart views
│   ├── Account/                   # Login and profile management
│
├── wwwroot/
│   ├── css/                       # Stylesheets
│   ├── js/                        # JavaScript files
│   ├── images/                    # Static assets and media
│
├── Data/
│   ├── AppDbContext.cs            # Database context and configurations
│
├── Services/
│   ├── ProductService.cs          # Business logic for products
│   ├── OrderService.cs            # Business logic for orders
│   ├── NotificationService.cs     # Handles real-time notifications
│
├── Tests/
│   ├── UnitTests/                 # Unit tests for controllers and services
│   ├── IntegrationTests/          # End-to-end integration tests
```

---

## 📸 Screenshots

### **Home Page**
![Home Page](images/home-page.png "E-Commerce Home Page")

### **Product Page**
![Product Page](images/product-page.png "Detailed Product View")

### **Admin Dashboard**
![Admin Dashboard](images/admin-dashboard.png "Admin Panel for Sellers")

---

## 🎥 Demo Video

Check out the [demo video](https://www.youtube.com/watch?v=your-video-id) showcasing the core features.

Alternatively, view it directly below:

```html
<iframe width="560" height="315" src="https://www.youtube.com/embed/your-video-id" frameborder="0" allowfullscreen></iframe>
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
   git clone https://github.com/your-username/ecommerce-web.git
   cd ecommerce-web
   ```

2. **Install Dependencies**:
   Open the solution file (`ECommerceWeb.sln`) in Visual Studio and restore NuGet packages.

3. **Configure the Database**:
   - Update the `appsettings.json` file with your database connection string:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
       }
     }
     ```
   - Apply migrations and update the database:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

4. **Run the Application**:
   Press `F5` or click the \"Run\" button in Visual Studio.

---

## 🛡️ Security Features

- Password hashing with **BCrypt**.
- JWT-based API authentication for secure endpoints.
- Sensitive keys managed via environment variables.

---

## 🌟 Planned Enhancements

- **Mobile Application**: Native iOS and Android apps for users and sellers.
- **Multi-Language Support**: Localized UI for international users.
- **Integration with Seller Automation System**: Real-time data synchronization with the desktop app.
- **AI-Powered Product Recommendations**: Personalized suggestions for users.

---

## 🤝 Contributing

We welcome contributions! To get started:
1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-name`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-name`).
5. Open a pull request.

---

## 📧 Contact

For inquiries, reach out via email: **tmenguc12@gmail.com**

---

## 📜 License

This project is licensed under the **MIT License**. See the `LICENSE` file for details.
```
