using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseAPI.Data
{
    public static class Seed
    {
        public static async Task SeedDataAsync(UserManager<AppUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    AppDbContext appContext)
        {
            var AdminRoleId = Guid.NewGuid();
            var StudentRoleId = Guid.NewGuid();
            var InstructorRoleId = Guid.NewGuid();
            var adminId = new Guid("9e47da69-3d3e-428d-a395-d53908753582");

            if (!await userManager.Users.AnyAsync())
            {
                //var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
                //var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
                //if (users == null) return;


                var user = new AppUser
                {
                    Id = adminId,
                    UserName = "admin123",
                    Email = "admin123@gmail.com",
                    NormalizedEmail = "admin123@gmail.com",
                };

                var roles = new List<IdentityRole<Guid>>
                {
                new IdentityRole<Guid>{Name = "Admin",Id = AdminRoleId,NormalizedName="Admin"},
                new IdentityRole<Guid>{Name = "Instructor", Id = InstructorRoleId,NormalizedName="Instructor"},
                new IdentityRole<Guid>{Name = "Student", Id = StudentRoleId, NormalizedName = "Student" },
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                //foreach (var user in users)
                //{
                //user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(userAdmin, "123");
                await userManager.AddToRoleAsync(userAdmin, "Admin");
                //}
                //add user Instructors
                var userInstructors = new List<AppUser>
                {
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753501"),
                        Fullname = "Elon Musk",
                        UserName = "ElonMusk@gmail.com",
                        Email = "ElonMusk@gmail.com",
                        NormalizedEmail = "ElonMusk@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753502"),
                        Fullname = "Jeff Bezos",
                        UserName = "JeffBezos@gmail.com",
                        Email = "JeffBezos@gmail.com",
                        NormalizedEmail = "JeffBezos@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753503"),
                        Fullname = "Bernard Arnault",
                        UserName = "BernardArnault@gmail.com",
                        Email = "BernardArnault@gmail.com",
                        NormalizedEmail = "BernardArnault@gmail.com",
                    }

                };
                foreach (var userInstructor in userInstructors)
                {
                    await userManager.CreateAsync(userInstructor, "123");
                    await userManager.AddToRoleAsync(userInstructor, "Instructor");
                }
                //add user Instructors
                var userStudents = new List<AppUser>
                {
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753504"),
                        Fullname = "Bill Gates",
                        UserName = "BillGates@gmail.com",
                        Email = "BillGates@gmail.com",
                        NormalizedEmail = "BillGates@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753505"),
                        Fullname = "Larry Page",
                        UserName = "LarryPage@gmail.com",
                        Email = "LarryPage@gmail.com",
                        NormalizedEmail = "LarryPage@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753506"),
                        Fullname = "Sergey Brin",
                        UserName = "SergeyBrin@gmail.com",
                        Email = "SergeyBrin@gmail.com",
                        NormalizedEmail = "SergeyBrin@gmail.com",
                    }

                };
                foreach (var userStudent in userStudents)
                {
                    await userManager.CreateAsync(userStudent, "123");
                    await userManager.AddToRoleAsync(userStudent, "Student");
                }
                //
            }



            if (!await appContext.Categories.AnyAsync())
            {
                // seed category
                await appContext.Categories.AddAsync(new Category(new Guid("9e47da69-3d3e-428d-a207-d53908753582"), "Development", null));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582")));

                await appContext.Categories.AddAsync(new Category(new Guid("9e47da02-3d3e-428d-a207-d53908753582"), "Business", null));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582")));

                await appContext.Categories.AddAsync(new Category(new Guid("9e47da02-3d3e-248d-a207-d53908753582"), "IT - SoftWare", null));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
                await appContext.Categories.AddAsync(new Category(Guid.NewGuid(), "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582")));
            }



            if (!await appContext.AudioLanguages.AnyAsync())
            {
                await appContext.AudioLanguages.AddAsync(new AudioLanguage
                {
                    Name = "English",
                });
                await appContext.AudioLanguages.AddAsync(new AudioLanguage
                {
                    Name = "Vietnamese",
                });
                await appContext.AudioLanguages.AddAsync(new AudioLanguage
                {
                    Name = "Japan",
                });
            }

            if (!await appContext.CloseCaptions.AnyAsync())
            {
                await appContext.CloseCaptions.AddAsync(new CloseCaption
                {
                    Name = "English",
                });
                await appContext.CloseCaptions.AddAsync(new CloseCaption
                {
                    Name = "Vietnamese",
                });
                await appContext.CloseCaptions.AddAsync(new CloseCaption
                {
                    Name = "Japan",
                });
            }


            if (!await appContext.Levels.AnyAsync())
            {
                await appContext.Levels.AddAsync(new Level
                {
                    Name = "Beginner",
                });
                await appContext.Levels.AddAsync(new Level
                {
                    Name = "Intermediate",
                });
                await appContext.Levels.AddAsync(new Level
                {
                    Name = "Expert",
                });

            }

            // seed 20 courses

            if (!await appContext.Courses.AnyAsync())
            {

                // seed course
                ///////////////////////////////////////////////////////////////////////////////////////
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = new Guid("9e47da02-3d3e-428d-a207-d53908753501"),
                    Title = "The Complete Financial Analyst Course 2022",
                    ShortDescription = "Excel, Accounting, Financial Statement Analysis, Business Analysis, Financial Math, PowerPoint: Everything is Included!",
                    Description = "The Complete Financial Analyst Course is the most comprehensive, dynamic, and practical course you will find online. It covers several topics, which are fundamental for every aspiring Financial Analyst: Microsoft Excel for beginner and intermediate users: become proficient with the world’s number 1 productivity software Accounting, financial statements, and financial ratios: making sense of debits and credits, profit and loss statements, balance sheets, liquidity, solvency, profitability, and growth financial ratios Finance basics: Interest rates, financial math calculations, loan calculations, time value of money, present and future value of cash flows Business analysis: Understanding what drives a business, key items to be analyzed and their meaning, the importance of industry cycles, important drivers for the business of startup, growth, mature and declining companies, industry KPIs Capital budgeting: Decide whether a company's project is feasible from a financial perspective and be able to compare between different investment opportunities Microsoft PowerPoint for beginner and intermediate users: The #1 tool for visual representation of your work, a necessary skill for every financial analyst As you can see, this is a complete bundle that ensures you will receive the right training for each critical aspect.",
                    Learn = "Work comfortably with Microsoft Excel. Format spreadsheets in a professional way. Be much faster carrying out regular tasks. Create professional charts in Microsoft Excel. Work with large amounts of data without difficulty. Understand Accounting and Bookkeeping principles.Build a company’s P & L from scratch. Build a company’s Balance sheet from scratch. Perform Financial statement analysis. Understand the importance of timing in terms of revenue and cost recognition. Calculate Liquidity, Solvency, Profitability, and Growth ratios to analyze a company’s performance. Understand 10 - K reports",
                    Requirement = "Absolutely no experience is required. We will start from the basics and gradually build up your knowledge. Everything is in the course. You will need Microsoft Excel 2010, 2013, 2016, or 2020 You will need Microsoft PowerPoint 2010, 2013, 2016, or 2020",
                    ThumbnailUrl = "https://img-c.udemycdn.com/course/240x135/648826_f0e5_4.jpg",
                    PreviewVideoUrl = "https://www.youtube.com/watch?v=9Fd9hw329fY&list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                    Price = 100,
                    CategoryId = Guid.Parse("9e47da69-3d3e-428d-a207-d53908753582"),
                    UserId = Guid.Parse("9e47da02-3d3e-428d-a207-d53908753501"),
                    Sections = new List<Section>()
                    {
                        new Section()
                        {
                            Title = "Welcome! Course Introduction",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "What Does the Course Cover?",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/9Fd9hw329fY?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/9Fd9hw329fY?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Everything We Will Learn Has a Practical Application",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/FfWpgLFMI7w?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/FfWpgLFMI7w?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "The Best Way to Take This Course",
                                    TotalTime = 336,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/fQXKjmCDkIA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/fQXKjmCDkIA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        new Section()
                        {
                            Id = new Guid("9e47da02-3d3e-428d-a207-d53908753502"),
                            Title = "Microsoft Excel - Quick Introduction",
                            CourseId=Guid.Parse("9e47da02-3d3e-428d-a207-d53908753501"),
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Microsoft Excel: The World's #1 Office Software",
                                    TotalTime = 336,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8392NJjj8s0?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8392NJjj8s0?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Excel Made Easy: A Beginner's Guide to Excel Spreadsheets",
                                    TotalTime = 656,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8392NJjj8s0?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8392NJjj8s0?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Data Entry Techniques in Excel",
                                    TotalTime = 896,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8392NJjj8s0?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8392NJjj8s0?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "How to Make Your Spreadsheets Look Professional",
                                    TotalTime = 322,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/McoDjOCb2Zo?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/McoDjOCb2Zo?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }

                            }
                        },
                        new Section()
                        {
                            Id = new Guid("9e47da02-3d3e-428d-a207-d53908753503"),
                            Title = "Microsoft Excel - Useful Tools",
                            CourseId=Guid.Parse("9e47da02-3d3e-428d-a207-d53908753501"),
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Inserting a Line Break with Alt + Enter",
                                    TotalTime = 352,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/1wn5Ur1_vKg?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/1wn5Ur1_vKg?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Do More with Your Sales Data with Excel's Text to Columns Feature",
                                    TotalTime = 358,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/C6jJg9Zan7w?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/C6jJg9Zan7w?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Create Easily Printable Excel Documents",
                                    TotalTime = 725,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/gE9bjYpUrNY?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/gE9bjYpUrNY?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "How to Wrap Text in Excel and Adjust a Cell's Size",
                                    TotalTime = 536,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/zfvxp7PgQ6c?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/zfvxp7PgQ6c?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        }
                    }
                });
                ///////////////////////////////////////////////////////////////////////////////////////
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = new Guid("9e47da02-3d3e-428d-a207-d53908753502"),
                    Title = "Become a Product Manager | Learn the Skills & Get the Job",
                    ShortDescription = "The most complete course available on Product Management. 13+ hours of videos, activities, interviews, & more",
                    Description = "**Updated June 2022: Over 4,000 students who have taken this course have gotten jobs as Product Managers! Students now work at companies like Google, Zynga, Airbnb, Wal-Mart, Dell, Booking. com, Jet. com, Vodafone, HomeAway, Boeing, Freelancer. com, Wayfair, & more!** The most updated and complete Product Management course on Udemy! You'll learn the skills that make up the entire Product Management job and process: from ideation to market research, to UX wireframing to prototyping, technology, metrics, and finally to building the product with user stories, project management, scoping, and leadership. We even have interviews with real life PMs, Q&A sessions with students, and a comprehensive guide to preparing and interviewing for a Product Management job. Right now, there are over 3,000+ job listings worldwide that are looking for Product Managers, that pay on average $100,000 / year.",
                    Learn = "Understand the varying role of a Product Manager through different types and sizes of companies Decide which type of Product Manager best fits one's goals and personality Understand the Product Lifecycle and how it applies to every product Understand the modern Product Development Process that both Fortune 500s and Startups adhere to Know how to identify ideas worth pursuing and dedicating resources to Understand how to get at the root of customer pain points Understand and communicate customer pain by type and frequency Assess the core problem of a product Find and compare competitors and competing products Differentiate between Direct, Indirect, Substitute, and Potential competitors Understand the process of Customer Development and how it relates to being a Product Manager How to find potential interviewees for product interviews, user tests, and exploratory interviews How to structure and run a customer interview How to model interview questions correctly while avoiding bias Navigate the four different types of customer interviews Find potential interviewees both internally and externally Write emails that will get users and potential customers to respond Build user personas based on both qualitative and quantitative data Understand the difference between a wireframe, a mockup, and a prototype Sketch out a wireframe with just a pen and paper",
                    Requirement = "No pre-requisites, although familiarity with basic business concepts is helpful",
                    ThumbnailUrl = "https://img-c.udemycdn.com/course/240x135/673654_d677_7.jpg",
                    PreviewVideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                    Price = 300,
                    CategoryId = Guid.Parse("9e47da69-3d3e-428d-a207-d53908753582"),
                    UserId = Guid.Parse("9e47da02-3d3e-428d-a207-d53908753502"),
                    //
                    Sections = new List<Section>()
                    {
                        //
                        new Section()
                        {
                            Title = "Before Starting the Course",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Course Overview",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "First Thing to Do",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        ////
                        new Section()
                        {
                            Title = "Introduction to Product Management",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "What is a Product Manager?",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "What is a Product?",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        //////
                        new Section()
                        {
                            Title = "Introduction to Product Development",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "The Four Major Phases of the Product Lifecycle",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Product Lifecycle Phases: Real World Examples",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        }
                        //
                    },
                });
                ///////////////////////////////////////////////////////////////////////////////////////
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = new Guid("9e47da02-3d3e-428d-a207-d53908753503"),
                    Title = "100 Days of Code: The Complete Python Pro Bootcamp for 2022",
                    ShortDescription = "Master Python by building 100 projects in 100 days. Learn data science, automation, build websites, games and apps!",
                    Description = "Welcome to the 100 Days of Code - The Complete Python Pro Bootcamp, the only course you need to learn to code with Python. With over 500,000 5 STAR reviews and a 4.8 average, my courses are some of the HIGHEST RATED courses in the history of Udemy! 100 days, 1 hour per day, learn to build 1 project per day, this is how you master Python. At 60+ hours, this Python course is without a doubt the most comprehensive Python course available anywhere online. Even if you have zero programming experience, this course will take you from beginner to professional. Here's why:",
                    Learn = "You will master the Python programming language by building 100 unique projects over 100 days. You will learn automation, game, app and web development, data science and machine learning all using Python. You will be able to program in Python professionally You will learn Selenium, Beautiful Soup, Request, Flask, Pandas, NumPy, Scikit Learn, Plotly, and Matplotlib.",
                    Requirement = "No programming experience needed - I'll teach you everything you need to know A Mac or PC computer with access to the internet No paid software required - I'll teach you how to use PyCharm, Jupyter Notebooks and Google Colab I'll walk you through, step-by-step how to get all the software installed and set up",
                    ThumbnailUrl = "https://img-c.udemycdn.com/course/240x135/2776760_f176_10.jpg",
                    PreviewVideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                    Price = 300,
                    CategoryId = Guid.Parse("9e47da69-3d3e-428d-a207-d53908753582"),
                    UserId = Guid.Parse("9e47da02-3d3e-428d-a207-d53908753503"),
                    //
                    Sections = new List<Section>()
                    {
                        //
                        new Section()
                        {
                            Title = "Day 1 - Beginner - Working with Variables in Python to Manage Data",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "What you're going to get from this course",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "START HERE",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Downloadable Resources and Tips for Taking the Course",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        ////
                        new Section()
                        {
                            Title = "Day 2 - Beginner - Understanding Data Types and How to Manipulate Strings",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Day 2 Goals: what we will make by the end of the day",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Python Primitive Data Types",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        //////
                        new Section()
                        {
                            Title = "Day 3 - Beginner - Control Flow and Logical Operators",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Day 3 Goals: what we will make by the end of the day",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Control Flow with if / else and Conditional Operators",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Nested if statements and elif statements",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        }
                        //
                    },
                });
                ///////////////////////////////////////////////////////////////////////////////////////
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = new Guid("9e47da02-3d3e-428d-a207-d53908753504"),
                    Title = "Understanding APIs and RESTful APIs Crash Course",
                    ShortDescription = "An introduction to how APIs and RESTful APIs work. No coding in this course, it's all conceptual.",
                    Description = "In this course you will learn about APIs and RESTful APIs, and how they work. There is no coding in this course. By the end of this course you will completely understand how APIs work, and how computers talk to each other. You'll also be familiar with RESTful APIs which make use of the HTTP protocol. If that sounded confusing, scary, or overly technical — it's not — it's just a fancy way of saying.",
                    Requirement= "You should probably be an intermediate developer by now OR be a junior developer who's trying to level up their skills. OR You should be interested in learning more about communication between computers(you don't need to be a coder if you fit into this category)",
                    Learn = "What APIs are How APIs work What a RESTful API is What JSON is How computers talk to each other CRUD Operations The 5 main request methods (GET, POST, PUT/PATCH, DELETE) HTTP status codes How to understand APIs as if they were real people HTTP Requests and Responses",
                    ThumbnailUrl = "https://img-c.udemycdn.com/course/240x135/2425522_6d25.jpg",
                    PreviewVideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                    Price = 300,
                    CategoryId = Guid.Parse("9e47da69-3d3e-428d-a207-d53908753582"),
                    UserId = Guid.Parse("9e47da02-3d3e-428d-a207-d53908753502"),
                    //
                    Sections = new List<Section>()
                    {
                        //
                        new Section()
                        {
                            Title = "Introduction",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Introduction",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "What is an API?",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        ////
                        new Section()
                        {
                            Title = "Understanding APIs and RESTful APIs",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Introduction to RESTful APIs",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Introduction to JSON",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        //////
                        new Section()
                        {
                            Title = "Summary",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Resources",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "REST API Cheat Sheet",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        }
                        //
                    },
                });
                ///////////////////////////////////////////////////////////////////////////////////////
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = new Guid("9e47da02-3d3e-428d-a207-d53908753505"),
                    Title = "The Complete Financial Analyst Course 2022",
                    ShortDescription = "Excel, Accounting, Financial Statement Analysis, Business Analysis, Financial Math, PowerPoint: Everything is Included!",
                    Description = "** Updated for May 2022! **If you’re trying to prepare for a career in finance, but are still looking to round out your knowledge of the subject, The Complete Financial Analyst Course might be a perfect fit for you, Business Insider A Financial Analyst Career is one of the top - paying entry - level jobs on the market.",
                    Requirement = "Absolutely no experience is required. We will start from the basics and gradually build up your knowledge. Everything is in the course. You will need Microsoft Excel 2010, 2013, 2016, or 2020 You will need Microsoft PowerPoint 2010, 2013, 2016, or 2020",
                    Learn = "Work comfortably with Microsoft Excel Format spreadsheets in a professional way Be much faster carrying out regular tasks Create professional charts in Microsoft Excel Work with large amounts of data without difficulty Understand Accounting and Bookkeeping principles Build a company’s P&L from scratch Build a company’s Balance sheet from scratch",
                    ThumbnailUrl = "https://img-c.udemycdn.com/course/240x135/648826_f0e5_4.jpg",
                    PreviewVideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                    Price = 300,
                    CategoryId = Guid.Parse("9e47da69-3d3e-428d-a207-d53908753582"),
                    UserId = Guid.Parse("9e47da02-3d3e-428d-a207-d53908753501"),
                    //
                    Sections = new List<Section>()
                    {
                        //
                        new Section()
                        {
                            Title = "Welcome! Course Introduction",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "What Does the Course Cover?",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Everything We Will Learn Has a Practical Application",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        ////
                        new Section()
                        {
                            Title = "Microsoft Excel - Quick Introduction",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Microsoft Excel: The World's #1 Office Softwar",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Excel Made Easy: A Beginner's Guide to Excel Spreadsheets",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        //////
                        new Section()
                        {
                            Title = "Microsoft Excel - Useful Tools",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Inserting a Line Break with Alt + Enter",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Do More with Your Sales Data with Excel's Text to Columns Feature",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        }
                        //
                    },
                });
                ///////////////////////////////////////////////////////////////////////////////////////
                await appContext.Courses.AddAsync(new Courses()
                {
                    Id = new Guid("9e47da02-3d3e-428d-a207-d53908753506"),
                    Title = "Beginner to Pro in Excel: Financial Modeling and Valuation",
                    ShortDescription = "Financial Modeling in Excel that would allow you to walk into a job and be a rockstar from day one!",
                    Description = "o you want to learn how to use Excel in a real working environment? Are you about to graduate from university and start looking for your first job? Are you a young professional looking to establish yourself in your new position? Would you like to become your team's go-to person when it comes to Financial Modeling in Excel?",
                    Requirement = "Microsoft Excel 2010, Microsoft Excel 2013, Microsoft Excel 2016, or Microsoft Excel 2020",
                    Learn = "Master Microsoft Excel and many of its advanced features Become one of the top Excel users in your team Carry out regular tasks faster than ever before Build P&L statements from a raw data extraction Acquire financial modeling skills Discover how to value a company Build Valuation models from scratch Create models with multiple scenarios",
                    ThumbnailUrl = "https://img-c.udemycdn.com/course/240x135/321410_d9c5_4.jpg",
                    PreviewVideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                    Price = 300,
                    CategoryId = Guid.Parse("9e47da69-3d3e-428d-a207-d53908753582"),
                    UserId = Guid.Parse("9e47da02-3d3e-428d-a207-d53908753502"),
                    //
                    Sections = new List<Section>()
                    {
                        //
                        new Section()
                        {
                            Title = "Beginner to Pro in Excel: Financial Modeling and Valuation - Welcome!",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Bonus! Welcome gift",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Welcome gift number 2",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/w-OKdSHRlfA?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        ////
                        new Section()
                        {
                            Title = "Introduction to Excel",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Introduction to Excel",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Overview of Excel",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        },
                        //////
                        new Section()
                        {
                            Title = "The Excel ribbon",
                            Lectures = new List<Lecture>()
                            {
                                new Lecture()
                                {
                                    Title = "Basic operations with rows and columns",
                                    TotalTime = 736,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                },
                                new Lecture()
                                {
                                    Title = "Data entry in Excel",
                                    TotalTime = 436,
                                    IsPreview = true,
                                    VideoUrl = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC",
                                    VideoPoster = "https://youtu.be/8ztq9fQT6Kc?list=PLWKjhJtqVAbmqFs83T4W-FZQ9kK983tZC"
                                }
                            }
                        }
                        //
                    },
                });
                
                ///////////////////////////////////////////////////////////////////////////////////////
            }

            await appContext.SaveChangesAsync();
        }
    }
}
