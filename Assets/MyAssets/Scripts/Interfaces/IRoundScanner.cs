using System;
using System.Collections.Generic;

public interface IRoundScanner
{
    List<string> Tags { get; set; }
    Action OnSuccess { get; set; }
    Action OnFailure { get; set; }
}