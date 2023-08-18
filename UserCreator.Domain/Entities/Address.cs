﻿namespace UserCreator.Domain.Entities;

public class Address
{
    public string Id { get; set; }

    public int UserId { get; set; }

    public string Street { get; set; }

    public int Number { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public User User { get; set; }
}

