using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Enigma_Protocol.Models
{
    public class User
    {
        public User(int userId, string userName, string passwordHash, string email,
            string shippingAddress, DateTime createdAt, List<Order> orders, List<Cart> carts)
        {
            UserId = userId;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            ShippingAddress = shippingAddress;
            CreatedAt = createdAt;
            Orders = orders;
            Carts = carts;
        }

        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Order> Orders { get; set; }
        public List<Cart> Carts { get; set; }

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
