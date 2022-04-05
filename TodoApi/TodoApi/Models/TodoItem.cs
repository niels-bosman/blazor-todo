﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models;

public class TodoItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string? Title { get; set; }

    public string Date { get; set; }
    
    public bool Finished { get; set; }
}