﻿
// This file was auto-generated by ML.NET Model Builder. 

using MLModel_ConsoleApp1;
using System.IO;

// Create single instance of sample data from first line of dataset for model input
var imageBytes = File.ReadAllBytes(@"E:\temp\MLNetImageClassification\ImageClassificationWebSite\Images\Cat\Cat1.jpg");
MLModel.ModelInput sampleData = new MLModel.ModelInput()
{
    ImageSource = imageBytes,
};

// Make a single prediction on the sample data and print results.
var sortedScoresWithLabel = MLModel.PredictAllLabels(sampleData);
Console.WriteLine($"{"Class",-40}{"Score",-20}");
Console.WriteLine($"{"-----",-40}{"-----",-20}");

foreach (var score in sortedScoresWithLabel)
{
    Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
}
