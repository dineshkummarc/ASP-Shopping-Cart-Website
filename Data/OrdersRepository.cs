using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Views;

namespace Data
{
    public class OrdersRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public OrdersRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public OrdersRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Retrieves all possible order statuses.
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type OrderStatus.</returns>
        public IQueryable<OrderStatus> RetrieveAllOrderStatuses()
        {
            try
            {
                return Entities.OrderStatuses.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves an Order Status by ID.
        /// Level: Data
        /// </summary>
        /// <param name="StatusID">The statuses ID.</param>
        /// <returns>An OrderStatus object.</returns>
        public OrderStatus RetrieveOrderStatusByID(int StatusID)
        {
            try
            {
                return Entities.OrderStatuses.SingleOrDefault(s => s.Id == StatusID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves an Order by ID.
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <returns>The Order Object</returns>
        public Order RetrieveOrderByID(Guid OrderID)
        {
            try
            {
                return Entities.Orders.SingleOrDefault(o => o.Id == OrderID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User's Orders
        /// Level: Data
        /// </summary>
        /// <param name="Username">The User's Username</param>
        /// <returns>A collection of type UserOrdersView</returns>
        public IQueryable<UserOrdersView> RetrieveOrdersByUsername(string Username)
        {
            try
            {
                User myUser = new UsersRepository().RetrieveUserByUsername(Username);
                List<UserOrdersView> myOrderListViews = new List<UserOrdersView>();

                if (myUser != null)
                {
                    List<Order> myOrderList = Entities.Orders.Where(o => o.UserFK == myUser.Id).ToList();

                    foreach (Order myCurrentOrder in myOrderList)
                    {
                        UserOrdersView myOrdersView = new UserOrdersView();

                        myOrdersView.Id = myCurrentOrder.Id;
                        myOrdersView.OrderDate = myCurrentOrder.OrderDate.ToShortDateString();
                        myOrdersView.OrderStatus = RetrieveOrderStatusByID(myCurrentOrder.OrderStatusFK).Status;
                        myOrdersView.Username = new UsersRepository().RetrieveUserById(Guid.Parse(myCurrentOrder.UserFK.ToString())).UserDetail.Username;

                        myOrderListViews.Add(myOrdersView);
                    }
                }

                return myOrderListViews.AsQueryable<UserOrdersView>();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Supplier's Orders
        /// Level: Data
        /// </summary>
        /// <param name="Supplier">The supplier's Name</param>
        /// <returns>A collection of SupplierOrdersView</returns>
        public IQueryable<SupplierOrdersView> RetrieveOrdersBySupplier(string Supplier)
        {
            try
            {
                List<SupplierOrdersView> myOrderListViews = new List<SupplierOrdersView>();
                List<Order> myOrderList = Entities.Orders.Where(o => o.Supplier.Supplier1.Contains(Supplier)).ToList();

                foreach (Order myCurrentOrder in myOrderList)
                {
                    SupplierOrdersView myOrdersView = new SupplierOrdersView();

                    myOrdersView.Id = myCurrentOrder.Id;
                    myOrdersView.OrderDate = myCurrentOrder.OrderDate.ToShortDateString();
                    myOrdersView.OrderStatus = RetrieveOrderStatusByID(myCurrentOrder.OrderStatusFK).Status;
                    myOrdersView.Supplier = new SuppliersRepository().RetrieveSupplierByID(Convert.ToInt32(myCurrentOrder.SupplierFK)).Supplier1;

                    myOrderListViews.Add(myOrdersView);
                }

                return myOrderListViews.AsQueryable<SupplierOrdersView>();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds an Order to the Database
        /// Level: Data
        /// </summary>
        /// <param name="myOrder">The order to add</param>
        public void AddOrder(Order myOrder)
        {
            try
            {
                Entities.AddToOrders(myOrder);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves an Order Staus by Name
        /// Level: Data
        /// </summary>
        /// <param name="Status">The Order Status</param>
        /// <returns>The Order Staus Object</returns>
        public OrderStatus RetrieveStatusByName(string Status)
        {
            try
            {
                return Entities.OrderStatuses.SingleOrDefault(s => s.Status == Status);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Increases a Products Stock Quantity
        /// Level: Data
        /// </summary>
        /// <param name="ProductFK">The Product's ID</param>
        /// <param name="Quantity">The Quantity to Increase By</param>
        public void IncreaseStockQuantity(Guid ProductFK, int Quantity)
        {
            try
            {
                Product myProduct = Entities.Products.SingleOrDefault(p => p.Id == ProductFK);

                myProduct.StockQuantity += Quantity;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Decreases a Products Stock Quantity
        /// Level: Data
        /// </summary>
        /// <param name="ProductFK">The Product's ID</param>
        /// <param name="Quantity">The Quantity to Decrease By</param>
        public void DecreaseStockQuantity(Guid ProductFK, int Quantity)
        {
            try
            {
                Product myProduct = Entities.Products.SingleOrDefault(p => p.Id == ProductFK);

                if (myProduct.StockQuantity > myProduct.ReorderLevel) //if larger than threshold 
                {
                    if ((myProduct.StockQuantity - Quantity) <= myProduct.ReorderLevel) //and now less than threshold
                    {

                        new Mailing().LowProductMail(myProduct.Name, myProduct.Supplier.Supplier1, "michael.cassar@icloud.com");
                     
                    }
                }

                myProduct.StockQuantity -= Quantity;
                
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds Order Items to a Supplier Order
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="myOrderItems">A Collection of OrderItem</param>
        public void AddOrderItems(Guid OrderID, List<OrderItem> myOrderItems)
        {
            try
            {
                foreach (OrderItem myOrderItem in myOrderItems)
                {
                    OrderProduct myProduct = new OrderProduct();

                    myProduct.OrderFK = OrderID;
                    myProduct.ProductFK = myOrderItem.Id;
                    myProduct.Quantity = myOrderItem.Quantity;
                    myProduct.Price = myOrderItem.Price;

                    Entities.AddToOrderProducts(myProduct);

                    IncreaseStockQuantity(myOrderItem.Id, myOrderItem.Quantity);
                }

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds Order Items to A User Order
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="myOrderItems">A collection of OrderItem</param>
        /// <returns>True if Insufficient Stock</returns>
        public bool AddUserOrderItems(Guid OrderID, List<OrderItem> myOrderItems)
        {
            try
            {
                bool StockQuantityMismatch = false;

                foreach (OrderItem myOrderItem in myOrderItems)
                {
                    OrderProduct myProduct = new OrderProduct();

                    myProduct.OrderFK = OrderID;
                    myProduct.ProductFK = myOrderItem.Id;
                    myProduct.Quantity = myOrderItem.Quantity;
                    myProduct.Price = myOrderItem.Price;

                    Entities.AddToOrderProducts(myProduct);

                    if (HasSufficientQuantity(myOrderItem.Id, myProduct.Quantity))
                    {
                        DecreaseStockQuantity(myOrderItem.Id, myOrderItem.Quantity);
                    }
                    else
                    {
                        StockQuantityMismatch = true;
                    }
                }

                Entities.SaveChanges();

                return StockQuantityMismatch; //If Mismatch then Rollback and return false;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a Product Has Sufficient Quantity
        /// Level: Data
        /// </summary>
        /// <param name="ProductID">The Product's ID</param>
        /// <param name="Quantity">The Quantity</param>
        /// <returns>True if Sufficient Quantity. False if Insufficient Quantity.</returns>
        public bool HasSufficientQuantity(Guid ProductID, int Quantity)
        {
            try
            {
                Product myProduct = Entities.Products.SingleOrDefault(p => p.Id == ProductID);

                if ((myProduct.StockQuantity - Quantity) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates an Orders Order Status
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="StatusID">The Status ID</param>
        public void UpdateOrderStatus(Guid OrderID, int StatusID)
        {
            try
            {
                Order myOrder = RetrieveOrderByID(OrderID);

                myOrder.OrderStatusFK = StatusID;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves and Orders Items
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <returns>A collection of type OrderProduct</returns>
        public IQueryable<OrderProduct> RetrieveItemsByOrderID(Guid OrderID)
        {
            try
            {
                return Entities.OrderProducts.Where(op => op.OrderFK == OrderID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a User Order Item
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="NewQuantity">The New Quantity</param>
        /// <returns>True if Successful. False if Unsuccessful.</returns>
        public bool UpdateUserOrderItem(Guid OrderID, Guid ProductID, int NewQuantity)
        {
            try
            {
                OrderProduct myOrderProduct = Entities.OrderProducts.SingleOrDefault(op => op.OrderFK == OrderID && op.ProductFK == ProductID);

                int OldQuantity = myOrderProduct.Quantity;

                if (NewQuantity < OldQuantity)
                {
                    IncreaseStockQuantity(ProductID, (OldQuantity - NewQuantity));
                    myOrderProduct.Quantity = NewQuantity;

                    Entities.SaveChanges();
                    return true;
                }
                else if (OldQuantity < NewQuantity)
                {
                    if (HasSufficientQuantity(ProductID, (NewQuantity - OldQuantity)))
                    {
                        DecreaseStockQuantity(ProductID, (NewQuantity - OldQuantity));
                        myOrderProduct.Quantity = NewQuantity;

                        Entities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Supplier Order Item
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="NewQuantity">The New Quantity</param>
        /// <returns>True if Successful. False if Unsuccessful.</returns>
        public bool UpdateSupplierOrderItem(Guid OrderID, Guid ProductID, int NewQuantity)
        {
            try
            {
                OrderProduct myOrderProduct = Entities.OrderProducts.SingleOrDefault(op => op.OrderFK == OrderID && op.ProductFK == ProductID);

                int OldQuantity = myOrderProduct.Quantity;

                if (NewQuantity < OldQuantity)
                {
                    if (HasSufficientQuantity(ProductID, (OldQuantity - NewQuantity)))
                    {
                        DecreaseStockQuantity(ProductID, OldQuantity - NewQuantity);

                        myOrderProduct.Quantity = NewQuantity;

                        Entities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (OldQuantity < NewQuantity)
                {
                    IncreaseStockQuantity(ProductID, (NewQuantity - OldQuantity));

                    myOrderProduct.Quantity = NewQuantity;

                    Entities.SaveChanges();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes a User Order Item
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        public void RemoveUserOrderItem(Guid OrderID, Guid ProductID)
        {
            try
            {
                OrderProduct myOrderProduct = Entities.OrderProducts.SingleOrDefault(op => op.OrderFK == OrderID && op.ProductFK == ProductID);

                IncreaseStockQuantity(ProductID, myOrderProduct.Quantity);

                Entities.DeleteObject(myOrderProduct);

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes a Supplier Order Item
        /// Level: Data
        /// </summary>
        /// <param name="OrderID">Order ID</param>
        /// <param name="ProductID">Product ID</param>
        /// <returns>True if Successful. False if Unsuccessful.</returns>
        public bool RemoveSupplierOrderItem(Guid OrderID, Guid ProductID)
        {
            try
            {
                OrderProduct myOrderProduct = Entities.OrderProducts.SingleOrDefault(op => op.OrderFK == OrderID && op.ProductFK == ProductID);

                if (HasSufficientQuantity(ProductID, myOrderProduct.Quantity))
                {
                    DecreaseStockQuantity(ProductID, myOrderProduct.Quantity);
                    Entities.DeleteObject(myOrderProduct);
                    Entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves the Stock History.
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type StockHistoryView</returns>
        public IQueryable<StockHistoryView> RetrieveStockHistory()
        {
            try
            {
                List<Order> myOrders = Entities.Orders.ToList();
                IQueryable<OrderProduct> myOrderProducts = Entities.OrderProducts;

                List<StockHistoryView> myStockHistoryList = new List<StockHistoryView>();

                foreach (OrderProduct myOrderItem in myOrderProducts)
                {
                    StockHistoryView myStockHistoryItem = new StockHistoryView();

                    myStockHistoryItem.Date = myOrders.SingleOrDefault(o => o.Id == myOrderItem.OrderFK).OrderDate;
                    myStockHistoryItem.Product = new ProductsRepository().RetrieveProductByID(myOrderItem.ProductFK).Name;

                    Order myCurrentOrder = RetrieveOrderByID(myOrderItem.OrderFK);

                    if (myCurrentOrder.UserFK == null)
                    {
                        myStockHistoryItem.AffectedBy = "Supplier Order";
                        myStockHistoryItem.QuantityIncreased = myOrderItem.Quantity;
                        myStockHistoryItem.QuantityDecreases = 0;
                    }
                    else
                    {
                        myStockHistoryItem.AffectedBy = new UsersRepository().RetrieveUserById(Guid.Parse(myCurrentOrder.UserFK.ToString())).UserDetail.Username;
                        myStockHistoryItem.QuantityIncreased = 0;
                        myStockHistoryItem.QuantityDecreases = myOrderItem.Quantity;
                    }

                    myStockHistoryList.Add(myStockHistoryItem);
                }

                myStockHistoryList.Sort((x, y) => y.Date.CompareTo(x.Date));

                return myStockHistoryList.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieve Stock History For Product
        /// Level: Data
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>A collection of type StockHistoryView</returns>
        public IQueryable<StockHistoryView> RetrieveStockHistoryForProduct(Guid ProductID)
        {
            try
            {
                List<Order> myOrders = Entities.Orders.ToList();
                IQueryable<OrderProduct> myOrderProducts = Entities.OrderProducts.Where(op => op.ProductFK == ProductID);

                List<StockHistoryView> myStockHistoryList = new List<StockHistoryView>();

                foreach (OrderProduct myOrderItem in myOrderProducts)
                {
                    StockHistoryView myStockHistoryItem = new StockHistoryView();

                    myStockHistoryItem.Date = myOrders.SingleOrDefault(o => o.Id == myOrderItem.OrderFK).OrderDate;
                    myStockHistoryItem.Product = new ProductsRepository().RetrieveProductByID(myOrderItem.ProductFK).Name;

                    Order myCurrentOrder = RetrieveOrderByID(myOrderItem.OrderFK);

                    if (myCurrentOrder.UserFK == null)
                    {
                        myStockHistoryItem.AffectedBy = "Supplier Order";
                        myStockHistoryItem.QuantityIncreased = myOrderItem.Quantity;
                        myStockHistoryItem.QuantityDecreases = 0;
                    }
                    else
                    {
                        myStockHistoryItem.AffectedBy = new UsersRepository().RetrieveUserById(Guid.Parse(myCurrentOrder.UserFK.ToString())).UserDetail.Username;
                        myStockHistoryItem.QuantityIncreased = 0;
                        myStockHistoryItem.QuantityDecreases = myOrderItem.Quantity;
                    }

                    myStockHistoryList.Add(myStockHistoryItem);
                }

                myStockHistoryList.Sort((x, y) => y.Date.CompareTo(x.Date));

                return myStockHistoryList.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves the Top Ten
        /// Level: Data
        /// </summary>
        /// <param name="StartDate">The Start Date</param>
        /// <param name="EndDate">The End Date</param>
        /// <returns>A collection of Type TopTenView</returns>
        public IQueryable<TopTenView> RetrieveTopTen(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                List<OrderProduct> myOrderItems = new List<OrderProduct>();

                if (StartDate.ToShortDateString() == EndDate.ToShortDateString())
                {
                    myOrderItems = Entities.OrderProducts.Where(op => op.Order.OrderDate >= StartDate && op.Order.SupplierFK == null).ToList();
                }
                else
                {
                    myOrderItems = Entities.OrderProducts.Where(op => op.Order.OrderDate >= StartDate && op.Order.OrderDate <= EndDate && op.Order.SupplierFK == null).ToList();
                }

                List<TopTenView> myTopTen = new List<TopTenView>();

                foreach (OrderProduct myOrderItem in myOrderItems)
                {
                    TopTenView myTopTenView = new TopTenView();

                    myTopTenView.ProductID = myOrderItem.ProductFK;
                    myTopTenView.ProductName = new ProductsRepository().RetrieveProductByID(myOrderItem.ProductFK).Name;
                    myTopTenView.ProductDescription = new ProductsRepository().RetrieveProductByID(myOrderItem.ProductFK).Description;
                    myTopTenView.QuantitySold = myOrderItem.Quantity;

                    if (myTopTen.SingleOrDefault(t => t.ProductID == myOrderItem.ProductFK) != null)
                    {
                        TopTenView myExistingProduct = myTopTen.SingleOrDefault(t => t.ProductID == myOrderItem.ProductFK);

                        myExistingProduct.QuantitySold += myOrderItem.Quantity;
                    }
                    else
                    {
                        myTopTen.Add(myTopTenView);
                    }
                }

                myTopTen.Sort((x, y) => y.QuantitySold.CompareTo(x.QuantitySold));

                return myTopTen.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves the TopTenClients
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type TopTenClients</returns>
        public IQueryable<TopTenClients> RetrieveTopTenClients()
        {
            try
            {
                List<TopTenClients> myTopTen = new List<TopTenClients>();

                List<OrderProduct> myOrderItems = Entities.OrderProducts.ToList();

                foreach (OrderProduct myCurrentOrderItem in myOrderItems)
                {
                    if (myCurrentOrderItem.Order.SupplierFK == null)
                    {
                        if (myTopTen.SingleOrDefault(c => c.ClientID == myCurrentOrderItem.Order.UserFK) != null)
                        {
                            TopTenClients myTopTenClients = myTopTen.SingleOrDefault(c => c.ClientID == myCurrentOrderItem.Order.UserFK);

                            myTopTenClients.ProductCount += myCurrentOrderItem.Quantity;
                        }
                        else
                        {
                            TopTenClients myTopTenClients = new TopTenClients();
                            User myUser = new UsersRepository().RetrieveUserById(Guid.Parse(myCurrentOrderItem.Order.UserFK.ToString()));

                            myTopTenClients.ClientID = Guid.Parse(myCurrentOrderItem.Order.UserFK.ToString());
                            myTopTenClients.ClientName = myUser.Name + " " + myUser.Surname;
                            myTopTenClients.ProductCount += myCurrentOrderItem.Quantity;

                            myTopTen.Add(myTopTenClients);
                        }
                    }
                }

                myTopTen.Sort((x, y) => y.ProductCount.CompareTo(x.ProductCount));

                return myTopTen.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Last User Order
        /// Level: Data;
        /// </summary>
        /// <param name="Username">User's Username</param>
        /// <returns>Order ID</returns>
        public Guid RetrieveLastUserOrder(string Username)
        {
            List<Order> myOrders = Entities.Orders.Where(o => o.User.UserDetail.Username == Username).ToList();

            return myOrders.OrderByDescending(c => c.OrderDate).FirstOrDefault().Id;
        }
    }
}
