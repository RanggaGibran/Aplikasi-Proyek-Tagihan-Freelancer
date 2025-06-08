# FreelancePro - Aplikasi Proyek & Tagihan Freelancer

🚀 **Modern Freelancer Project & Invoice Management System** - A self-hosted, beautifully designed solution for managing clients, projects, tasks, and invoices with a stunning dark theme UI.

## ✨ Key Features

- **Modern Dark UI**: Beautiful, responsive dark theme with gradient accents
- **Client Management**: Comprehensive client information management
- **Project Management**: Create and monitor projects from start to finish
- **Task Management**: Track task progress within each project
- **Invoice System**: Create and manage professional invoices
- **Dashboard**: Visual overview with statistics and quick actions
- **Responsive Design**: Perfect experience on desktop, tablet, and mobile
- **Real-time Updates**: Blazor Server for instant UI updates
- **No Monthly Fees**: Self-hosted solution, pay once and own forever

## 🎨 Design Features

- **Modern Dark Theme**: Sleek dark interface with purple/blue gradients
- **Glassmorphism Effects**: Beautiful backdrop blur and transparency effects
- **Smooth Animations**: Fluid transitions and hover effects
- **Card-based Layout**: Clean, organized information presentation
- **Gradient Accents**: Eye-catching purple-to-blue color schemes
- **Professional Typography**: Clean, readable fonts with proper hierarchy

## 🛠️ Technology Stack

- **Framework**: .NET 9.0 Blazor Server
- **Database**: Entity Framework Core with In-Memory provider
- **UI Framework**: Bootstrap 5.3
- **Icons**: Bootstrap Icons
- **Styling**: Custom CSS with modern dark theme
- **Deployment**: Docker support, Heroku ready

## 🚀 Quick Start

### Prasyarat

- .NET 9.0 SDK atau lebih baru
- Visual Studio 2022 atau VS Code
- Git (untuk version control)

### Instalasi Lokal

1. **Clone repository**
   ```bash
   git clone https://github.com/yourusername/aplikasi-proyek-tagihan-freelancer.git
   cd aplikasi-proyek-tagihan-freelancer
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build aplikasi**
   ```bash
   dotnet build
   ```

4. **Jalankan aplikasi**
   ```bash
   dotnet run --project "Aplikasi Proyek & Tagihan Freelancer"
   ```

5. **Akses aplikasi**
   Buka browser dan kunjungi: `https://localhost:5001` atau `http://localhost:5000`

## 🌐 Deploy ke Heroku

### 1. Persiapan File untuk Heroku

Pastikan Anda memiliki file-file berikut:

**Procfile** (buat di root directory):
```
web: dotnet "Aplikasi Proyek & Tagihan Freelancer/bin/Release/net9.0/Aplikasi_Proyek_&_Tagihan_Freelancer.dll" --server.urls http://+:$PORT
```

### 2. Setup Heroku

1. **Install Heroku CLI**
   - Download dari: https://devcenter.heroku.com/articles/heroku-cli

2. **Login ke Heroku**
   ```bash
   heroku login
   ```

3. **Buat aplikasi Heroku**
   ```bash
   heroku create nama-aplikasi-anda
   ```

4. **Set buildpack untuk .NET**
   ```bash
   heroku buildpacks:set https://github.com/heroku/heroku-buildpack-dotnetcore
   ```

5. **Deploy aplikasi**
   ```bash
   git add .
   git commit -m "Initial deploy"
   git push heroku main
   ```

### 3. Konfigurasi Environment Variables

```bash
heroku config:set ASPNETCORE_ENVIRONMENT=Production
heroku config:set ASPNETCORE_URLS=http://+:$PORT
```

## 📁 Struktur Proyek

```
Aplikasi Proyek & Tagihan Freelancer/
├── Components/
│   ├── Layout/           # Layout komponen
│   └── Pages/            # Halaman Razor
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   ├── Client.cs         # Model klien
│   ├── Project.cs        # Model proyek
│   ├── ProjectTask.cs    # Model tugas
│   ├── Invoice.cs        # Model tagihan
│   └── Enums.cs          # Enum status
├── Services/
│   ├── ClientService.cs  # Service klien
│   ├── ProjectService.cs # Service proyek
│   └── TaskService.cs    # Service tugas
└── wwwroot/
    └── app.css           # Styling kustom
```

## 🎨 Screenshot

*Screenshot aplikasi akan ditambahkan setelah deployment*

## 🔧 Konfigurasi

### Database

Aplikasi ini menggunakan Entity Framework Core dengan In-Memory database untuk development. Untuk production, Anda dapat mengkonfigurasi database yang sesuai di `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Production-Database-Connection-String"
  }
}
```

### Environment Variables

- `ASPNETCORE_ENVIRONMENT`: Set ke `Production` untuk deployment
- `ASPNETCORE_URLS`: URL binding untuk aplikasi

## 🤝 Kontribusi

1. Fork repository ini
2. Buat branch feature (`git checkout -b feature/AmazingFeature`)
3. Commit perubahan (`git commit -m 'Add some AmazingFeature'`)
4. Push ke branch (`git push origin feature/AmazingFeature`)
5. Buat Pull Request

## 📝 Roadmap

- [ ] Sistem autentikasi dan autorisasi
- [ ] Export invoice ke PDF
- [ ] Notifikasi email otomatis
- [ ] Laporan keuangan
- [ ] API REST untuk integrasi
- [ ] Mode dark/light theme
- [ ] Multi-language support
- [ ] Advanced filtering dan search

## 📜 Lisensi

Proyek ini dilisensikan di bawah [MIT License](LICENSE).

## 🙋‍♂️ Kontak

**Rangga Gibran** - [Email ME!](mailto:anggakatio73@gmail.com)

Project Link: [https://github.com/RanggaGibran/aplikasi-proyek-tagihan-freelancer](https://github.com/RanggaGibran/aplikasi-proyek-tagihan-freelancer)

---

⭐ **Jika proyek ini membantu Anda, berikan star di GitHub!** ⭐
