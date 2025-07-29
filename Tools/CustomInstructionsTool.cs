// Copyright © Trimble Inc.
// All rights reserved.
// The entire contents of this file is protected by U.S. and
// International Copyright Laws. Unauthorized reproduction,
// reverse-engineering, and distribution of all or any portion of
// the code contained in this file is strictly prohibited and may
// result in severe civil and criminal penalties and will be
// prosecuted to the maximum extent possible under the law.
//
// CONFIDENTIALITY
//
// This source code and all resulting intermediate files, as well as the
// application design, are confidential and proprietary trade secrets of
// Trimble Inc.

using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Tools;

[McpServerToolType]
public sealed class CustomInstructionsTool
{
    [McpServerTool(Name = "get-instructions"), Description("Returns the contents of copilot-instructions.md.")]
    public static string GetInstructions()
    {
        Console.WriteLine("GetInstructions method invoked.");
        var rootPath = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.GetFullPath(Path.Combine(rootPath, "../../../.github/copilot-instructions.md"));
        Console.WriteLine($"Attempting to read: {filePath}");
        try
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("copilot-instructions.md found, returning contents.");
                return File.ReadAllText(filePath);
            }
            else
            {
                Console.Error.WriteLine($"File not found at path: {filePath}");
                return $"Error: File not found at path: {filePath}";
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file: {ex.Message}");
            return $"Error reading file: {ex.Message}";
        }
    }
}
