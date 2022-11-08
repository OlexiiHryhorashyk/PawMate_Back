﻿namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
        public virtual List<Liked> Liked { get; set; }
    }
}
