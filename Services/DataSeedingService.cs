using Aplikasi_Proyek___Tagihan_Freelancer.Data;
using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public class DataSeedingService
{
    private readonly ApplicationDbContext _context;

    public DataSeedingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        // Check if data already exists
        if (_context.Clients.Any())
        {
            return; // Data already seeded
        }        // Create sample clients
        var clients = new List<Client>
        {
            new Client
            {
                Id = Guid.NewGuid(),
                Name = "PT. Technology Indonesia",
                Email = "contact@techindo.com",
                PhoneNumber = "+62-21-1234567",
                Company = "PT. Technology Indonesia",
                CreatedAt = DateTime.UtcNow.AddMonths(-3)
            },
            new Client
            {
                Id = Guid.NewGuid(),
                Name = "CV. Digital Solutions",
                Email = "info@digitalsol.com",
                PhoneNumber = "+62-22-9876543",
                Company = "CV. Digital Solutions",
                CreatedAt = DateTime.UtcNow.AddMonths(-2)
            },
            new Client
            {
                Id = Guid.NewGuid(),
                Name = "Start-up Innovate",
                Email = "hello@innovate.id",
                PhoneNumber = "+62-21-5555666",
                Company = "Start-up Innovate",
                CreatedAt = DateTime.UtcNow.AddMonths(-1)
            }
        };

        _context.Clients.AddRange(clients);
        await _context.SaveChangesAsync();

        // Create sample projects
        var projects = new List<Project>
        {
            new Project
            {
                Id = Guid.NewGuid(),
                ClientId = clients[0].Id,
                Name = "Website Company Profile",
                Description = "Pembuatan website company profile dengan fitur CMS dan responsive design",
                Status = ProjectStatus.Aktif,
                TotalValue = 15000000,
                StartDate = DateTime.UtcNow.AddMonths(-2),
                Deadline = DateTime.UtcNow.AddMonths(1),
                DueDate = DateTime.UtcNow.AddMonths(1),
                Budget = 20000000,
                HourlyRate = 150000,
                CreatedAt = DateTime.UtcNow.AddMonths(-2)
            },
            new Project
            {
                Id = Guid.NewGuid(),
                ClientId = clients[1].Id,
                Name = "E-commerce Platform",
                Description = "Pengembangan platform e-commerce dengan fitur payment gateway dan inventory management",
                Status = ProjectStatus.Aktif,
                TotalValue = 50000000,
                StartDate = DateTime.UtcNow.AddMonths(-1),
                Deadline = DateTime.UtcNow.AddMonths(3),
                DueDate = DateTime.UtcNow.AddMonths(3),
                Budget = 60000000,
                HourlyRate = 200000,
                CreatedAt = DateTime.UtcNow.AddMonths(-1)
            },
            new Project
            {
                Id = Guid.NewGuid(),
                ClientId = clients[2].Id,
                Name = "Mobile App Development",
                Description = "Pembuatan aplikasi mobile untuk iOS dan Android dengan fitur real-time chat",
                Status = ProjectStatus.Selesai,
                TotalValue = 30000000,
                StartDate = DateTime.UtcNow.AddMonths(-4),
                Deadline = DateTime.UtcNow.AddMonths(-1),
                DueDate = DateTime.UtcNow.AddMonths(-1),
                Budget = 35000000,
                HourlyRate = 175000,
                CreatedAt = DateTime.UtcNow.AddMonths(-4)
            }
        };

        _context.Projects.AddRange(projects);
        await _context.SaveChangesAsync();

        // Create sample tasks
        var tasks = new List<ProjectTask>
        {
            // Tasks for Website Company Profile
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[0].Id,
                Title = "Design UI/UX",
                Description = "Membuat design mockup dan wireframe untuk website",
                Status = Models.TaskStatus.Completed,
                Value = 3000000,
                DueDate = DateTime.UtcNow.AddDays(-15),
                EstimatedHours = 40,
                CreatedAt = DateTime.UtcNow.AddMonths(-2)
            },
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[0].Id,
                Title = "Frontend Development",
                Description = "Implementasi design ke dalam HTML, CSS, dan JavaScript",
                Status = Models.TaskStatus.InProgress,
                Value = 5000000,
                DueDate = DateTime.UtcNow.AddDays(10),
                EstimatedHours = 60,
                CreatedAt = DateTime.UtcNow.AddMonths(-1)
            },
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[0].Id,
                Title = "CMS Integration",
                Description = "Integrasi dengan content management system",
                Status = Models.TaskStatus.ToDo,
                Value = 4000000,
                DueDate = DateTime.UtcNow.AddDays(20),
                EstimatedHours = 50,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },

            // Tasks for E-commerce Platform
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[1].Id,
                Title = "Database Design",
                Description = "Merancang struktur database untuk e-commerce",
                Status = Models.TaskStatus.Completed,
                Value = 8000000,
                DueDate = DateTime.UtcNow.AddDays(-10),
                EstimatedHours = 30,
                CreatedAt = DateTime.UtcNow.AddMonths(-1)
            },
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[1].Id,
                Title = "Payment Gateway Integration",
                Description = "Integrasi dengan berbagai payment gateway (Midtrans, Xendit, dll)",
                Status = Models.TaskStatus.InProgress,
                Value = 15000000,
                DueDate = DateTime.UtcNow.AddDays(30),
                EstimatedHours = 80,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[1].Id,
                Title = "Admin Dashboard",
                Description = "Pembuatan dashboard admin untuk mengelola produk dan pesanan",
                Status = Models.TaskStatus.ToDo,
                Value = 12000000,
                DueDate = DateTime.UtcNow.AddDays(45),
                EstimatedHours = 70,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },

            // Tasks for Mobile App (Completed project)
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[2].Id,
                Title = "App Architecture",
                Description = "Merancang arsitektur aplikasi mobile",
                Status = Models.TaskStatus.Completed,
                Value = 5000000,
                DueDate = DateTime.UtcNow.AddMonths(-3),
                EstimatedHours = 40,
                CreatedAt = DateTime.UtcNow.AddMonths(-4)
            },
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[2].Id,
                Title = "Real-time Chat Feature",
                Description = "Implementasi fitur chat real-time menggunakan Socket.IO",
                Status = Models.TaskStatus.Completed,
                Value = 10000000,
                DueDate = DateTime.UtcNow.AddMonths(-2),
                EstimatedHours = 80,
                CreatedAt = DateTime.UtcNow.AddMonths(-3)
            },
            new ProjectTask
            {
                Id = Guid.NewGuid(),
                ProjectId = projects[2].Id,
                Title = "App Store Deployment",
                Description = "Deploy aplikasi ke Google Play Store dan Apple App Store",
                Status = Models.TaskStatus.Completed,
                Value = 3000000,
                DueDate = DateTime.UtcNow.AddMonths(-1),
                EstimatedHours = 20,
                CreatedAt = DateTime.UtcNow.AddMonths(-2)
            }
        };        _context.ProjectTasks.AddRange(tasks);
        await _context.SaveChangesAsync();        // Create sample invoices
        var invoices = new List<Invoice>
        {
            new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-001",
                ProjectId = projects[2].Id, // Mobile App project (completed)
                IssueDate = DateTime.UtcNow.AddMonths(-1),
                DueDate = DateTime.UtcNow.AddDays(-15),
                Status = InvoiceStatus.Paid,
                Notes = "Invoice for completed mobile app development",
                CreatedAt = DateTime.UtcNow.AddMonths(-1)
            },
            new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-002",
                ProjectId = projects[0].Id, // E-commerce project (ongoing)
                IssueDate = DateTime.UtcNow.AddDays(-15),
                DueDate = DateTime.UtcNow.AddDays(15),
                Status = InvoiceStatus.Sent,
                Notes = "Partial payment for e-commerce development - Phase 1",
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-003",
                ProjectId = projects[1].Id, // Dashboard project 
                IssueDate = DateTime.UtcNow.AddDays(-7),
                DueDate = DateTime.UtcNow.AddDays(23),
                Status = InvoiceStatus.Draft,
                Notes = "Consultation services for system architecture review",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            }
        };

        _context.Invoices.AddRange(invoices);
        await _context.SaveChangesAsync();        // Create sample invoice items
        var invoiceItems = new List<InvoiceItem>
        {
            // Items for INV-001 (Mobile App - completed tasks)
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[0].Id,
                Description = "App Architecture Design",
                Quantity = 1,
                UnitPrice = 5000000
            },
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[0].Id,
                Description = "Real-time Chat Feature Implementation",
                Quantity = 1,
                UnitPrice = 10000000
            },
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[0].Id,
                Description = "App Store Deployment",
                Quantity = 1,
                UnitPrice = 3000000
            },

            // Items for INV-002 (E-commerce - partial)
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[1].Id,
                Description = "Homepage Design & Development",
                Quantity = 1,
                UnitPrice = 8000000
            },
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[1].Id,
                Description = "Product Catalog Implementation",
                Quantity = 1,
                UnitPrice = 7000000
            },

            // Items for INV-003 (Manual consultation)
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[2].Id,
                Description = "System Architecture Review",
                Quantity = 8,
                UnitPrice = 500000
            },
            new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoices[2].Id,
                Description = "Performance Optimization Consultation",
                Quantity = 4,
                UnitPrice = 750000
            }
        };        _context.InvoiceItems.AddRange(invoiceItems);
        await _context.SaveChangesAsync();

        // Update invoice totals
        invoices[0].TotalAmount = invoiceItems.Where(i => i.InvoiceId == invoices[0].Id).Sum(i => i.Total);
        invoices[1].TotalAmount = invoiceItems.Where(i => i.InvoiceId == invoices[1].Id).Sum(i => i.Total);
        invoices[2].TotalAmount = invoiceItems.Where(i => i.InvoiceId == invoices[2].Id).Sum(i => i.Total);

        _context.Invoices.UpdateRange(invoices);
        await _context.SaveChangesAsync();
    }
}
