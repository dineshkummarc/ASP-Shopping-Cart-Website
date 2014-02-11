using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Data;
using Common;
using Common.Views;

namespace Logic
{
    public class OrdersLogic
    {
        /// <summary>
        /// Adds an Order
        /// Level: Logic
        /// </summary>
        /// <param name="SupplierFK">Supplier ID</param>
        /// <param name="UserFK">User ID</param>
        /// <param name="myOrderItems">Collection of Order Items</param>
        /// <returns>True if successful false if not successful</returns>
        public bool AddOrder(int? SupplierFK, Guid? UserFK, string CreditCard, List<OrderItem> myOrderItems)
        {
            DbTransaction myTransaction = null;
            OrdersRepository myRepository = new OrdersRepository();

            myRepository.Entities.Connection.Open();

            using (myTransaction = myRepository.Entities.Connection.BeginTransaction())
            {
                try
                {
                    Order myOrder = new Order();

                    myOrder.Id = Guid.NewGuid();
                    myOrder.OrderDate = DateTime.Now;
                    myOrder.OrderStatusFK = myRepository.RetrieveStatusByName("Ordered").Id;
                    myOrder.UserFK = UserFK;
                    myOrder.SupplierFK = SupplierFK;

                    myRepository.AddOrder(myOrder);

                    if (SupplierFK != null)
                    {
                        myRepository.AddOrderItems(myOrder.Id, myOrderItems);

                        myTransaction.Commit();

                        myRepository.Entities.Connection.Close();

                        return true;
                    }
                    else
                    {
                        if (myRepository.AddUserOrderItems(myOrder.Id, myOrderItems)) // if mismatch occurs for quantity rollback 
                        {
                            myTransaction.Rollback();

                            myRepository.Entities.Connection.Close();

                            return false;
                        }
                        else //else commit changes
                        {
                            myTransaction.Commit();

                            new UsersLogic().InsertCreditCardNumber(CreditCard, Guid.Parse(UserFK.ToString()));

                            new ShoppingCartLogic().EmptyCart(Guid.Parse(UserFK.ToString()));

                            User myUser = new UsersRepository().RetrieveUserById(Guid.Parse(UserFK.ToString()));
                            new Mailing().PurchaseMail(myUser.UserDetail.Username, myUser.Email, myOrder.Id.ToString(), "michael.cassar@icloud.com");
                            
                            myRepository.Entities.Connection.Close();

                            return true;
                        }
                    }
                }
                catch (Exception Exception)
                {
                    if (myTransaction != null)
                    {
                        myTransaction.Rollback();
                    }

                    if (myRepository != null)
                    {
                        myRepository.Entities.Connection.Close();
                    }

                    throw Exception;
                }
            }
        }

        /// <summary>
        /// Retrieves an order by id
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The order id</param>
        /// <returns>An object of type order</returns>
        public Order RetrieveOrderByID(Guid OrderID)
        {
            try
            {
                return new OrdersRepository().RetrieveOrderByID(OrderID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a product has sufficient quantity
        /// Level: Logic
        /// </summary>
        /// <param name="ProductID">The product id</param>
        /// <param name="Quantity">The quantity</param>
        /// <returns>True if product has quantity false if it does not</returns>
        public bool HasSufficientQuantity(Guid ProductID, int Quantity)
        {
            try
            {
                return new OrdersRepository(false).HasSufficientQuantity(ProductID, Quantity);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all order statuses
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type OrderStatus</returns>
        public IQueryable<OrderStatus> RetrieveAllOrderStatuses()
        {
            try
            {
                return new OrdersRepository(false).RetrieveAllOrderStatuses();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves orders by Username
        /// Level: Logic
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <returns>A collection of type User Orders View</returns>
        public IQueryable<UserOrdersView> RetrieveOrdersByUsername(string Username)
        {
            try
            {
                return new OrdersRepository().RetrieveOrdersByUsername(Username);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves orders for supplier
        /// Level: Logic
        /// </summary>
        /// <param name="Supplier">The Supplier</param>
        /// <returns>A collection of type SupplierOrdersView</returns>
        public IQueryable<SupplierOrdersView> RetrieveOrdersBySupplier(string Supplier)
        {
            try
            {
                return new OrdersRepository().RetrieveOrdersBySupplier(Supplier);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates an Order Status
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="StatusID">The Order Status ID</param>
        public void UpdateOrderStatus(Guid OrderID, int StatusID)
        {
            try
            {
                new OrdersRepository().UpdateOrderStatus(OrderID, StatusID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Order Items By Order ID
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <returns>A collection of type OrderProduct</returns>
        public IQueryable<OrderProduct> RetrieveItemsByOrderID(Guid OrderID)
        {
            try
            {
                return new OrdersRepository().RetrieveItemsByOrderID(OrderID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates A User Order Item
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The order id</param>
        /// <param name="ProductID">The product id</param>
        /// <param name="NewQuantity">The new quantity</param>
        /// <returns>True if successful false if not</returns>
        public bool UpdateUserOrderItem(Guid OrderID, Guid ProductID, int NewQuantity)
        {
            DbTransaction myTransaction = null;
            OrdersRepository myRepository = new OrdersRepository();

            myRepository.Entities.Connection.Open();

            using (myTransaction = myRepository.Entities.Connection.BeginTransaction())
            {
                try
                {
                    bool Result = myRepository.UpdateUserOrderItem(OrderID, ProductID, NewQuantity);

                    if (Result == true)
                    {
                        myTransaction.Commit();

                        myRepository.Entities.Connection.Close();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception Exception)
                {
                    if (myTransaction != null)
                    {
                        myTransaction.Rollback();
                    }

                    if (myRepository != null)
                    {
                        myRepository.Entities.Connection.Close();
                    }

                    throw Exception;
                }
            }

            //return new OrdersRepository().UpdateUserOrderItem(OrderID, ProductID, NewQuantity);
        }

        /// <summary>
        /// Updates a Supplier Order Item
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="NewQuantity">The new quantity</param>
        /// <returns>True if update successful, false if not</returns>
        public bool UpdateSupplierOrderItem(Guid OrderID, Guid ProductID, int NewQuantity)
        {
            DbTransaction myTransaction = null;
            OrdersRepository myRepository = new OrdersRepository();

            myRepository.Entities.Connection.Open();

            using (myTransaction = myRepository.Entities.Connection.BeginTransaction())
            {
                try
                {
                    bool Result = myRepository.UpdateSupplierOrderItem(OrderID, ProductID, NewQuantity);

                    if (Result == true)
                    {
                        myTransaction.Commit();
                        
                        myRepository.Entities.Connection.Close();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception Exception)
                {
                    if (myTransaction != null)
                    {
                        myTransaction.Rollback();
                    }

                    if (myRepository != null)
                    {
                        myRepository.Entities.Connection.Close();
                    }

                    throw Exception;
                }
            }

            //return new OrdersRepository().UpdateSupplierOrderItem(OrderID, ProductID, NewQuantity);
        }

        /// <summary>
        /// Removes a user order item
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        public void RemoveUserOrderItem(Guid OrderID, Guid ProductID)
        {
            DbTransaction myTransaction = null;
            OrdersRepository myRepository = new OrdersRepository();

            myRepository.Entities.Connection.Open();

            using (myTransaction = myRepository.Entities.Connection.BeginTransaction())
            {
                try
                {
                    myRepository.RemoveUserOrderItem(OrderID, ProductID);

                    myTransaction.Commit();

                    myRepository.Entities.Connection.Close();
                }
                catch (Exception Exception)
                {
                    if (myTransaction != null)
                    {
                        myTransaction.Rollback();
                    }

                    if (myRepository != null)
                    {
                        myRepository.Entities.Connection.Close();
                    }

                    throw Exception;
                }
            }

            //new OrdersRepository().RemoveUserOrderItem(OrderID, ProductID);
        }

        /// <summary>
        /// Removes a supplier order item
        /// Level: Logic
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>True if successful false if not</returns>
        public bool RemoveSupplierOrderItem(Guid OrderID, Guid ProductID)
        {
            DbTransaction myTransaction = null;
            OrdersRepository myRepository = new OrdersRepository();

            myRepository.Entities.Connection.Open();

            using (myTransaction = myRepository.Entities.Connection.BeginTransaction())
            {
                try
                {
                    bool Result = myRepository.RemoveSupplierOrderItem(OrderID, ProductID);

                    if (Result == true)
                    {
                        myTransaction.Commit();
                        
                        myRepository.Entities.Connection.Close();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception Exception)
                {
                    if (myTransaction != null)
                    {
                        myTransaction.Rollback();
                    }

                    if (myRepository != null)
                    {
                        myRepository.Entities.Connection.Close();
                    }

                    throw Exception;
                }
            }

            //return new OrdersRepository().RemoveSupplierOrderItem(OrderID, ProductID);
        }

        /// <summary>
        /// Retrieves all stock history
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type StockHistoryView</returns>
        public IQueryable<StockHistoryView> RetrieveStockHistory()
        {
            try
            {
                return new OrdersRepository().RetrieveStockHistory();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Stock History For Product
        /// Level: Logic
        /// </summary>
        /// <param name="ProductID">Product ID</param>
        /// <returns>A collection of type Stock History View</returns>
        public IQueryable<StockHistoryView> RetrieveStockHistoryForProduct(Guid ProductID)
        {
            try
            {
                return new OrdersRepository().RetrieveStockHistoryForProduct(ProductID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Top Ten Products Sold
        /// Level: Logic
        /// </summary>
        /// <param name="StartDate">The Start Date</param>
        /// <param name="EndDate">The End Date</param>
        /// <returns>A collection of type TopTenView</returns>
        public IQueryable<TopTenView> RetrieveTopTen(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                return new OrdersRepository().RetrieveTopTen(StartDate, EndDate);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Top Ten Clients
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type TopTenClients</returns>
        public IQueryable<TopTenClients> RetrieveTopTenClients()
        {
            try
            {
                return new OrdersRepository().RetrieveTopTenClients();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Last User Order
        /// Level: Logic;
        /// </summary>
        /// <param name="Username">User's Username</param>
        /// <returns>Order ID</returns>
        public Guid RetrieveLastUserOrder(string Username)
        {
            return new OrdersRepository().RetrieveLastUserOrder(Username);
        }
    }
}
