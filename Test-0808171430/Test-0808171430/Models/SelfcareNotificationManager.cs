using System;
using System.Collections.Generic;
using Portal.SelfcareNotificationManager.Interface;
using Test_0808171430.Models;
using System.Linq;
using System.Text.RegularExpressions;
using Test_0808171430.Data;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace Portal.SelfcareNotificationManager
{
    public class SelfcareNotificationManager : ISelfcareNotification
    {
        private const string DESCRIPTION = "Компонет выполняет следующие функции: Создание уведомления для пользователя, получение списка уведомлений для пользователя, запрос текста уведомления по GUID, удаление уведомления.";
        //private ILogger _logger;        
        private SelfcareNotificationContext context;

        public SelfcareNotificationManager(string name, IDictionary<string, string> parameters) 
            //: base(name, DESCRIPTION, parameters)
        {
            //RegisterInterface<IWebUploadFileManager>(this);
            //if (Parameters["ContentDirectory"] != null)
            //    contentDirectory = parameters["ContentDirectory"];
            
            context = new SelfcareNotificationContext();
        }

        //public void CreateNotification(Customer customer, SelfcareNotification message)
        public void CreateNotification(Customer customer, string message)
        {            
            if (customer == null)
                return;

            if (string.IsNullOrWhiteSpace(message))
                return;

            // Get Header of user message            
            string header = Regex.Split(message, "\r\n|\r|\n").FirstOrDefault();

            // Get Body of user message
            int linesCount = 1;
            string[] lines = Regex.Split(message, "\r\n|\r|\n").Skip(linesCount).ToArray();

            // Create Notification
            var notification = new SelfcareNotification
            {
                CustomerID = customer.Id,
                Created = DateTime.Now,
                Header = header,
                Text = string.Join(Environment.NewLine, lines),
                Readed = null,
                GUID = Guid.NewGuid()
            };

            // An attempt to creeate the messages from the database        
            try
            {
                context.SelfcareNotifications.Add(notification);
                context.SaveChanges();
            }
            catch (DbUpdateException duex)
            {
                Console.WriteLine($"Create operation failed. {duex.ToString()}");
            }
        }

        public void Delete(string message)
        {
            // Get Body of user message
            int linesCount = 1;
            string[] lines = Regex.Split(message, "\r\n|\r|\n").Skip(linesCount).ToArray();
            
            string bodyMessage = string.Join(Environment.NewLine, lines);

            // An attempt to delete the messages from the database           
            try
            {
                // Search matching bodyMessage message in the DataContext
                SelfcareNotification notification = context.SelfcareNotifications.FirstOrDefault(x => x.Text == bodyMessage);                                   
                context.SelfcareNotifications.Remove(notification);
                context.SaveChanges();
            }
            catch (DataException dex)
            {
                Console.WriteLine($"Delete operation failed. {dex.ToString()}");                
            }
        }

        public IList<SelfcareNotification> Read(Customer customer)
        {
            // An attempt to read the messages from the database
            IList<SelfcareNotification> notification = null;
            try
            {                
                notification = context.SelfcareNotifications.Where(x => x.CustomerID == customer.Id).ToList();                                
            }
            catch (DbUpdateException duex)
            {
                Console.WriteLine($"Read operation failed. {duex.ToString()}");
            }

            return notification;
        }

        public SelfcareNotification ReadByGuid(Customer customer, Guid guid)
        {
            // An attempt to read the messages from the database
            SelfcareNotification notification = null;
            try
            {
                notification = context.SelfcareNotifications
                    .FirstOrDefault(x => (x.CustomerID == customer.Id) && (x.GUID == guid));

                // Return NULL If the notification was not found
                if (notification == null)
                {
                    return notification;
                }                
            }
            catch (DbUpdateException duex)
            {
                Console.WriteLine($"Read operation failed. {duex.ToString()}");
            }
            
            if (notification.Readed == null)
            {
                notification.Readed = DateTime.Now;
                context.SaveChanges();                
            }

            return notification;
        }
    }

}
