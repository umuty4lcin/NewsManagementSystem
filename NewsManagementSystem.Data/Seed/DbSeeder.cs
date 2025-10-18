using Microsoft.AspNetCore.Identity;
using NewsManagementSystem.Core.Entities;
using NewsManagementSystem.Data.Context;

namespace NewsManagementSystem.Data.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Editor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Editor"));
            }

            // Seed Users
            var users = new List<AppUser>();

            // Admin User
            if (await userManager.FindByEmailAsync("admin@haberportal.com") == null)
            {
                var adminUser = new AppUser
                {
                    UserName = "admin@haberportal.com",
                    Email = "admin@haberportal.com",
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    users.Add(adminUser);
                }
            }

            // Editor Users
            var editorEmails = new[]
            {
                "editor1@haberportal.com",
                "editor2@haberportal.com",
                "editor3@haberportal.com"
            };

            var editorNames = new[]
            {
                ("Fatma", "Demir"),
                ("Mehmet", "Kaya"),
                ("Ayşe", "Özkan")
            };

            for (int i = 0; i < editorEmails.Length; i++)
            {
                if (await userManager.FindByEmailAsync(editorEmails[i]) == null)
                {
                    var editorUser = new AppUser
                    {
                        UserName = editorEmails[i],
                        Email = editorEmails[i],
                        FirstName = editorNames[i].Item1,
                        LastName = editorNames[i].Item2,
                        EmailConfirmed = true,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };

                    var result = await userManager.CreateAsync(editorUser, "Editor123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(editorUser, "Editor");
                        users.Add(editorUser);
                    }
                }
            }

            // Seed Categories
            var categories = new List<Category>();
            if (!context.Categories.Any())
            {
                categories = new List<Category>
                {
                    new Category { Name = "Teknoloji", Description = "Teknoloji, bilim ve inovasyon haberleri", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Spor", Description = "Futbol, basketbol ve diğer spor dalları", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Ekonomi", Description = "Ekonomi, finans ve iş dünyası haberleri", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Sağlık", Description = "Sağlık, tıp ve wellness haberleri", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Eğitim", Description = "Eğitim, öğretim ve akademik gelişmeler", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Kültür", Description = "Sanat, müzik, sinema ve edebiyat", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Politika", Description = "Siyaset ve güncel olaylar", CreatedDate = DateTime.Now, IsActive = true },
                    new Category { Name = "Dünya", Description = "Uluslararası haberler ve gelişmeler", CreatedDate = DateTime.Now, IsActive = true }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
            else
            {
                categories = context.Categories.Where(c => c.IsActive).ToList();
            }

            // Seed News
            if (!context.News.Any())
            {
                var allUsers = context.Users.ToList();
                var newsList = new List<News>();

                // Teknoloji Haberleri
                var techCategory = categories.FirstOrDefault(c => c.Name == "Teknoloji");
                if (techCategory != null)
                {
                    newsList.AddRange(new[]
                    {
                        new News
                        {
                            Title = "Yapay Zeka Devrimi: ChatGPT'nin Yeni Versiyonu",
                            Summary = "OpenAI'ın geliştirdiği yeni yapay zeka modeli, doğal dil işleme alanında çığır açıyor.",
                            Content = "Yapay zeka teknolojisindeki son gelişmeler, insanlığın geleceğini şekillendirmeye devam ediyor. OpenAI'ın geliştirdiği yeni ChatGPT versiyonu, daha gelişmiş anlama ve üretme yetenekleriyle dikkat çekiyor. Bu teknoloji, eğitimden sağlığa, iş dünyasından sanata kadar birçok alanda devrim yaratıyor. Uzmanlar, yapay zekanın insanların günlük yaşamlarını nasıl değiştireceğini araştırıyor.",
                            Author = "Dr. Selin Tekin",
                            CategoryId = techCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/technology.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddDays(-2),
                            ViewCount = 1250,
                            CreatedDate = DateTime.Now.AddDays(-2),
                            IsActive = true
                        },
                        new News
                        {
                            Title = "5G Teknolojisi Türkiye'de Yaygınlaşıyor",
                            Summary = "Türkiye'de 5G altyapısının kurulumu hızla devam ediyor. İstanbul ve Ankara'da test süreçleri başladı.",
                            Content = "5G teknolojisinin Türkiye'deki yaygınlaşma süreci hızla devam ediyor. Telekomünikasyon şirketleri, büyük şehirlerde 5G altyapısının kurulumunu tamamlıyor. Bu teknoloji, internet hızında 10 kat artış sağlayacak ve nesnelerin interneti (IoT) uygulamalarının gelişimini hızlandıracak. Uzmanlar, 5G'nin sağlık, eğitim ve ulaşım sektörlerinde büyük değişimler yaratacağını öngörüyor.",
                            Author = "Mühendis Ali Vural",
                            CategoryId = techCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/5g-technology.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddDays(-1),
                            ViewCount = 890,
                            CreatedDate = DateTime.Now.AddDays(-1),
                            IsActive = true
                        }
                    });
                }

                // Spor Haberleri
                var sportCategory = categories.FirstOrDefault(c => c.Name == "Spor");
                if (sportCategory != null)
                {
                    newsList.AddRange(new[]
                    {
                        new News
                        {
                            Title = "Galatasaray Avrupa Ligi'nde Büyük Zafer",
                            Summary = "Sarı-kırmızılılar, Avrupa Ligi'nde önemli bir galibiyet elde ederek bir üst tura çıkma şansını artırdı.",
                            Content = "Galatasaray, Avrupa Ligi grup maçlarında büyük bir performans sergileyerek önemli bir galibiyet elde etti. Takım, teknik direktör Okan Buruk'un liderliğinde organize futbol oynayarak rakiplerini zor durumda bıraktı. Taraftarlar, takımın bu performansından memnun olduklarını belirtirken, yönetim de gelecek hedeflerini açıkladı.",
                            Author = "Spor Muhabiri Emre Çelik",
                            CategoryId = sportCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/football.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddHours(-6),
                            ViewCount = 2100,
                            CreatedDate = DateTime.Now.AddHours(-6),
                            IsActive = true
                        },
                        new News
                        {
                            Title = "Olimpiyat Hazırlıkları Hızla Devam Ediyor",
                            Summary = "Türk sporcuları, 2024 Olimpiyat Oyunları için yoğun antrenman programlarına devam ediyor.",
                            Content = "Türkiye'nin en başarılı sporcuları, 2024 Paris Olimpiyat Oyunları için hazırlıklarını sürdürüyor. Jimnastik, yüzme, atletizm ve güreş branşlarında umut vaat eden sporcularımız, uluslararası müsabakalarda ülkemizi temsil etmeye hazırlanıyor. Milli takım antrenörleri, sporcuların performansından memnun olduklarını belirtirken, madalya hedeflerini açıkladı.",
                            Author = "Olimpiyat Muhabiri Zeynep Arslan",
                            CategoryId = sportCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/olympics.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddHours(-12),
                            ViewCount = 1560,
                            CreatedDate = DateTime.Now.AddHours(-12),
                            IsActive = true
                        }
                    });
                }

                // Ekonomi Haberleri
                var economyCategory = categories.FirstOrDefault(c => c.Name == "Ekonomi");
                if (economyCategory != null)
                {
                    newsList.AddRange(new[]
                    {
                        new News
                        {
                            Title = "Borsa İstanbul'da Yeni Rekor",
                            Summary = "BIST 100 endeksi, güçlü yatırımcı ilgisiyle yeni rekor seviyelerine ulaştı.",
                            Content = "Borsa İstanbul'da işlem gören hisse senetleri, güçlü yatırımcı ilgisiyle değer kazanmaya devam ediyor. BIST 100 endeksi, günlük işlemlerde yeni rekor seviyelerine ulaşırken, yatırımcıların güvenini kazanıyor. Finans uzmanları, bu yükselişin sürdürülebilir olup olmadığını değerlendiriyor. Bankacılık ve teknoloji sektörleri, en çok talep gören hisseler arasında yer alıyor.",
                            Author = "Ekonomi Editörü Murat Özdemir",
                            CategoryId = economyCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/stock-market.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddDays(-1),
                            ViewCount = 980,
                            CreatedDate = DateTime.Now.AddDays(-1),
                            IsActive = true
                        },
                        new News
                        {
                            Title = "Dijital Para Birimleri Yükselişte",
                            Summary = "Kripto para piyasasında yaşanan gelişmeler, yatırımcıların ilgisini artırıyor.",
                            Content = "Dijital para birimleri, son dönemde yaşanan teknolojik gelişmeler ve kurumsal yatırımlarla birlikte değer kazanmaya devam ediyor. Bitcoin ve diğer kripto paralar, yatırımcıların portföylerinde önemli yer tutuyor. Uzmanlar, bu piyasanın volatilitesine dikkat çekerek, yatırımcıları bilinçli kararlar vermeye çağırıyor.",
                            Author = "Finans Analisti Deniz Kılıç",
                            CategoryId = economyCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/cryptocurrency.jpg",
                            IsPublished = false,
                            PublishedDate = null,
                            ViewCount = 0,
                            CreatedDate = DateTime.Now.AddHours(-3),
                            IsActive = true
                        }
                    });
                }

                // Sağlık Haberleri
                var healthCategory = categories.FirstOrDefault(c => c.Name == "Sağlık");
                if (healthCategory != null)
                {
                    newsList.AddRange(new[]
                    {
                        new News
                        {
                            Title = "Yeni Kanser Tedavi Yöntemi Umut Veriyor",
                            Summary = "Bilim insanları, kanser tedavisinde devrim yaratacak yeni bir yöntem geliştirdi.",
                            Content = "Tıp dünyasında kanser tedavisi alanında önemli bir gelişme kaydedildi. Araştırmacılar, bağışıklık sistemini güçlendirerek kanser hücrelerini hedef alan yeni bir tedavi yöntemi geliştirdi. Bu yöntem, özellikle ileri evre kanser hastalarında umut verici sonuçlar gösteriyor. Klinik deneyler devam ederken, uzmanlar bu tedavinin yakında yaygın kullanıma gireceğini öngörüyor.",
                            Author = "Prof. Dr. Ayşe Kaya",
                            CategoryId = healthCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/medical-research.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddDays(-3),
                            ViewCount = 3400,
                            CreatedDate = DateTime.Now.AddDays(-3),
                            IsActive = true
                        },
                        new News
                        {
                            Title = "Mental Sağlık Farkındalığı Artıyor",
                            Summary = "Toplumda mental sağlık konusundaki farkındalık, uzman desteğiyle birlikte artıyor.",
                            Content = "Modern yaşamın getirdiği stres ve zorluklar, mental sağlık konusundaki farkındalığın artmasına neden oluyor. Uzmanlar, düzenli egzersiz, dengeli beslenme ve profesyonel destek almanın önemine dikkat çekiyor. Psikologlar, toplumun bu konudaki duyarlılığının artmasından memnun olduklarını belirtirken, daha fazla kaynak ayrılması gerektiğini vurguluyor.",
                            Author = "Psikolog Dr. Elif Yılmaz",
                            CategoryId = healthCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/mental-health.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddHours(-8),
                            ViewCount = 1200,
                            CreatedDate = DateTime.Now.AddHours(-8),
                            IsActive = true
                        }
                    });
                }

                // Eğitim Haberleri
                var educationCategory = categories.FirstOrDefault(c => c.Name == "Eğitim");
                if (educationCategory != null)
                {
                    newsList.AddRange(new[]
                    {
                        new News
                        {
                            Title = "Dijital Eğitim Platformları Yaygınlaşıyor",
                            Summary = "Pandemi sonrası dijital eğitim platformlarının kullanımı artarak devam ediyor.",
                            Content = "Dijital eğitim platformları, öğrencilerin eğitim süreçlerinde önemli bir rol oynamaya devam ediyor. Online kurslar, interaktif içerikler ve sanal sınıf uygulamaları, eğitimi daha erişilebilir hale getiriyor. Eğitim uzmanları, hibrit eğitim modelinin geleceğin eğitim sistemi olacağını öngörüyor. Öğretmenler de bu teknolojilere adapte olarak eğitim kalitesini artırıyor.",
                            Author = "Eğitim Uzmanı Prof. Dr. Mustafa Öztürk",
                            CategoryId = educationCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/digital-education.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddDays(-4),
                            ViewCount = 2100,
                            CreatedDate = DateTime.Now.AddDays(-4),
                            IsActive = true
                        }
                    });
                }

                // Kültür Haberleri
                var cultureCategory = categories.FirstOrDefault(c => c.Name == "Kültür");
                if (cultureCategory != null)
                {
                    newsList.AddRange(new[]
                    {
                        new News
                        {
                            Title = "İstanbul Film Festivali Başlıyor",
                            Summary = "42. İstanbul Film Festivali, dünyaca ünlü yönetmenlerin katılımıyla başlıyor.",
                            Content = "İstanbul Film Festivali, sinema dünyasının önemli isimlerini bir araya getiriyor. Bu yıl 42. kez düzenlenen festival, yerli ve yabancı filmlerin yanı sıra atölyeler ve panel tartışmalarına da ev sahipliği yapacak. Festival direktörü, sinema sanatının toplumsal etkisine dikkat çekerek, bu etkinliğin kültürel zenginliğimize katkısını vurguladı.",
                            Author = "Kültür Muhabiri Gülay Arslan",
                            CategoryId = cultureCategory.Id,
                            UserId = allUsers.FirstOrDefault()?.Id ?? "",
                            ImageUrl = "/uploads/news/film-festival.jpg",
                            IsPublished = true,
                            PublishedDate = DateTime.Now.AddHours(-4),
                            ViewCount = 890,
                            CreatedDate = DateTime.Now.AddHours(-4),
                            IsActive = true
                        }
                    });
                }

                context.News.AddRange(newsList);
                await context.SaveChangesAsync();
            }
        }
    }
}

