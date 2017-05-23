﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace UAParser.Tests
{
  public class ParserTests
  {
    [Fact]
    public void can_get_default_parser()
    {
      Parser parser = Parser.GetDefault();
      Assert.NotNull(parser);
    }

    [Fact]
    public void can_get_parser_from_input()
    {
      string yamlContent = this.GetTestResources("UAParser.Tests.Regexes.regexes.yaml");
      Parser parser = Parser.FromYaml(yamlContent);
      Assert.NotNull(parser);
    }

    [Fact]
    public void can_get_parser_from_file()
    {
        string yamlContent = this.GetTestResources("UAParser.Tests.Regexes.regexes.yaml");
        string path = Path.GetTempFileName();
        File.WriteAllText(path, yamlContent, Encoding.UTF8);
        Parser parser = Parser.FromYamlFile(path);
        Assert.NotNull(parser);
        File.Delete(path);
    }

    [Fact]
    public void ignore_case_test()
    {
      Parser parser = Parser.GetDefault(ignoreCase: true);
      var client = parser.Parse("mozilla/5.0 (windows; u; en-us) applewebkit/531.9 (khtml, like gecko) adobeair/2.5.1");
      Assert.Equal("AdobeAIR", client.UserAgent.Family, StringComparer.OrdinalIgnoreCase);
      Assert.Equal("Windows", client.OS.Family, StringComparer.OrdinalIgnoreCase);
    }
  }
}
