﻿namespace AioCore.Shared.ValueObjects;

public class Response<T>
{
    public T? Data { get; set; }

    public string? Message { get; set; }

    public bool Success { get; set; }
}