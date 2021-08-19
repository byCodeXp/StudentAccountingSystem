using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Seed
    {
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seed(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<DataContext>();
            _roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        }

        public async Task InvokeAsync()
        {
            // Create roles

            await EnsureCreateRoles(
                AppEnv.Roles.Admin,
                AppEnv.Roles.Customer
            );

            // Create categories

            var figmaCategory = new Category { Name = "Figma" };
            var designCategory = new Category { Name = "Design" };
            var uiUxCategory = new Category { Name = "UI/UX" };
            var tsCategory = new Category { Name = "TypeScript" };
            var angularCategory = new Category { Name = "Angular" };
            var nodeCategory = new Category { Name = "Node.js" };
            var mongoCategory = new Category { Name = "MongoDb" };
            var devOpsCategory = new Category { Name = "DevOps" };
            var blenderCategory = new Category { Name = "Blender" };
            var unityCategory = new Category { Name = "Unity" };

            EnsureCreateCategories(
                figmaCategory,
                designCategory,
                uiUxCategory,
                tsCategory,
                angularCategory,
                nodeCategory,
                mongoCategory,
                devOpsCategory,
                blenderCategory,
                unityCategory
            );

            _context.SaveChanges();

            // Create courses

            var courses = new []
            {
                new Course
                {
                    Name = "Figma Crash Course",
                    Description = "Hi, in this course, you will learn how to create a Responsive Homepage using Figma",
                    Preview = "https://assets.website-files.com/5fbd9df89f668234dc47843b/5fbe80df178b6cb1606dcdff_OG-IMAGE.png",
                    Categories = new List<Category>() { figmaCategory, designCategory, uiUxCategory }
                },
                new Course
                {
                    Name = "TypeScript Crash Course",
                    Description = "This course was completely updated, reflects the latest version of TypeScript and incorporated tons of student feedback",
                    Preview = "https://www.freecodecamp.org/news/content/images/2021/03/typescript.png",
                    Categories = new List<Category>() { tsCategory }
                },
                new Course
                {
                    Name = "MEAN Stack E-Commerce App",
                    Description = "Build Great E-Shop with Admin Panel using the latest technologies: Nodejs , Mongo, Angular 12, Nrwl NX Monorepo, PrimeNG",
                    Preview = "https://cdn.skillenza.com/files/fb20e103-c185-49e9-b29d-f8c09752e047/meanstack_campaign_banner01.png",
                    Categories = new List<Category>() { angularCategory, nodeCategory, mongoCategory }
                },
                new Course
                {
                    Name = "Learn DevOps: The Complete Kubernetes Course",
                    Description = "Kubernetes will run and manage your containerized applications. Learn how to build, deploy, use, and maintain Kubernetes",
                    Preview = "https://app.softserveinc.com/apply/add/img/devops-crash-course-2.png",
                    Categories = new List<Category>() { devOpsCategory }
                },
                new Course
                {
                    Name = "Complete Blender Creator: Learn 3D Modelling for Beginners",
                    Description = "Use Blender to Create Beautiful 3D models for Video Games, 3D Printing & More. Beginners Level Course",
                    Preview = "https://www.blendernation.com/wp-content/uploads/2020/03/Promo-Thumbnail.jpg",
                    Categories = new List<Category>() { blenderCategory }
                },
                new Course
                {
                    Name = "Shader Development from Scratch for Unity with Cg",
                    Description = "Learn to program the graphics pipeline in Unity for creating unique visual surfaces for game objects",
                    Preview = "https://learningsolutionsmag.com/sites/default/files/article/id_7674_780.jpg",
                    Categories = new List<Category>() { unityCategory }
                }
            };

            foreach (var course in courses)
            {
                if (!_context.Courses.Any(m => m.Name == course.Name))
                {
                    _context.Courses.Add(course);
                }
            }

            _context.SaveChanges();
        }

        private void EnsureCreateCategories(params Category[] categories)
        {
            foreach (var category in categories)
            {
                if (!_context.Categories.Any(e => e.Name == category.Name))
                {
                    _context.Categories.Add(category);
                }
            }
        }

        private async Task EnsureCreateRoles(params string[] roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}