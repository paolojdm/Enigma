using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Enigma_Protocol.Models
{
    public class User
    {
        // Parameterless constructor
        public User()
        {
            Orders = new List<Order>();
            Carts = new List<Cart>();
        }

        public User(int userId, string userName, string passwordHash, string email,
            string shippingAddress, DateTime createdAt)
        {
            UserId = userId;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            ShippingAddress = shippingAddress;
            CreatedAt = createdAt;

            Orders = new List<Order>();
            Carts = new List<Cart>();

        }

        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; } // Navigation to Orders
        public virtual ICollection<Cart> Carts { get; set; }  // Navigation to Carts

    }
}

//UserId / Id { pk(int)}
//Username {string}
//PasswordHash {string}
//(email) { string}
//(Penalty) { Penalty(obj)}
//Role {string}
//ShippingAddress {string}
//CreatedAt {DateTime}
