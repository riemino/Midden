﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caf.Midden.Core.Services
{
    public class ProjectParser : IParseProjects
    {
        private const string PROJECT_VAR_NAME = "project";
        public Models.v0_2.Project Parse(string contents)
        {
            throw new NotImplementedException();
        }

        public Models.v0_2.Project Parse(StreamReader sr)
        {
            // Return null if not front-matter
            if (sr.ReadLine() != "---")
                return null;

            // Read through front-matter and try to find project name
            string? projectName = "";
            string? line;
            while((line = sr.ReadLine()) != null)
            {
                if (line == "---") break;
                if (line.StartsWith(PROJECT_VAR_NAME + ":"))
                {
                    projectName = ParseProjectName(line);
                }
            }

            // Return null if failed to find project name
            if (string.IsNullOrWhiteSpace(projectName))
                return null;

            // Found a project name, so create a project and get the contents
            Models.v0_2.Project project = new Models.v0_2.Project();
            project.Name = projectName;
            project.Description = sr.ReadToEnd();

            return project;
        }

        private string ParseProjectName(string line)
        {
            Regex regex = new Regex("\"(.*?)\"");

            var matches = regex.Matches(line);

            if(matches.Count > 0)
            {
                return matches[0].Groups[1].Value.Trim('"');
            }
            else { return null; }
        }
    }
}
