﻿namespace InfyKiddoFun.Domain.Wrapper;

public interface IResult
{
    List<string> Messages { get; set; }
    
    bool Succeeded { get; set; }
}

public interface IResult<T>
{
    T Data { get; set; }
}