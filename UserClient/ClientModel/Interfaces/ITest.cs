﻿namespace ClientModels
{
    public interface ITest
    {
        long testID { get; set; }
        string inputArgs { get; set; }
        string expectedOutput { get; set; }
    }
}