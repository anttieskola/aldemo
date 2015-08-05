﻿using aldemo.logic.Dal;
using aldemo.logic.Entities;
using ET.FakeText;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aldemo.generate
{
    public static class Generator
    {
        public static void Clean()
        {
            using (AssemblyContext ac = new AssemblyContext())
            {
                // status
                // projects
                foreach (var p in ac.Projects)
                {
                    ac.Entry<Project>(p).State = EntityState.Deleted;
                }
                ac.SaveChanges();
                // lines
                foreach (var l in ac.Lines)
                {
                    ac.Entry<Line>(l).State = EntityState.Deleted;
                }
                ac.SaveChanges();
            }
        }

        public static void Run(int pc, int lc)
        {
            using (AssemblyContext ac = new AssemblyContext())
            {
                TextGenerator tg = new TextGenerator();
                Random r = new Random();

                // lines
                List<Line> lines = new List<Line>();
                for (int i = 0; i < lc; i++)
                {
                    Line l = new Line
                    {
                        Name = tg.GenerateWord(r.Next(10, 20))
                    };
                    ac.Entry<Line>(l).State = EntityState.Added;
                    lines.Add(l);
                }
                ac.SaveChanges();

                // projects
                List<Project> projects = new List<Project>();
                for (int i = 0; i < pc; i++)
                {
                    Project p = new Project
                    {
                        Name = tg.GenerateWord(r.Next(20, 30)),
                        Lines = lines
                    };
                    ac.Entry<Project>(p).State = EntityState.Added;
                    projects.Add(p);
                }
                ac.SaveChanges();

                // statuses
            }
        }
    }
}
