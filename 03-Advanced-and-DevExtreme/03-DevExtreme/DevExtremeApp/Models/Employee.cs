using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace DevExtremeApp.Models;

public partial class Employee
{
    [JsonPropertyName("Id")]                        // Property name when serialized into JSON
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; } = null;

    [JsonPropertyName("Position")]
    public string Position { get; set; } = null;

    [JsonPropertyName("Salary")]
    public decimal Salary { get; set; }
}
