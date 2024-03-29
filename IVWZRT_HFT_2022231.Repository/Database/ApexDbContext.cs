﻿using Microsoft.EntityFrameworkCore;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public class ApexDbContext : DbContext
    {
        public ApexDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseInMemoryDatabase("apex")
                    .UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A player can play multiple matches, and a match contains multiple players
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Matches)
                .WithMany(m => m.Players)
                .UsingEntity<EndGameStat>(
                    s => s.HasOne(s => s.Match).WithMany().HasForeignKey(s => s.MatchId).OnDelete(DeleteBehavior.Cascade),
                    s => s.HasOne(s => s.Player).WithMany().HasForeignKey(s => s.PlayerId).OnDelete(DeleteBehavior.Cascade)
                );

            // An endgame stat belongs to 1 player
            modelBuilder.Entity<EndGameStat>()
                .HasOne(s => s.Player)
                .WithMany(p => p.Stats)
                .HasForeignKey(s => s.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            // An endgame stat is unique in a match
            modelBuilder.Entity<EndGameStat>()
                .HasOne(s => s.Match)
                .WithMany(m => m.Stats)
                .HasForeignKey(s => s.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // A player mains 1 legend, and a legend can be mained by multiple players
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Legend)
                .WithMany(l => l.Players)
                .HasForeignKey(p => p.LegendId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pre-defined entries in the database (randomly generated, apart from the usernames)
            Player[] players = new Player[]
            {
                new Player("1@SZ rpr@21@5538@8601@5459@Bronze@2"),
                new Player("2@no_lifey@21@5614@8833@6881@Bronze@30"),
                new Player("3@zicooooo@21@3475@4334@4705@Rookie@55"),
                new Player("4@WizardCoin23TTV@28@8268@8873@6259@Gold@17"),
                new Player("5@Shirat4m4@38@10275@8623@7509@Platinum@17"),
                new Player("6@Delunar@20@5919@5925@6396@Silver@41"),
                new Player("7@Harachan_QX@25@7330@5346@4789@Silver@6"),
                new Player("8@TragicRush@30@7838@10705@10926@Gold@52"),
                new Player("9@PUBSKI@18@9204@6733@4708@Gold@1"),
                new Player("10@Thesh1ro@29@10244@6971@7589@Platinum@21"),
                new Player("11@AegisAnder@25@13910@9903@6700@Master@56"),
                new Player("12@LFT_Natsonuharu@37@10751@11346@5001@Platinum@49"),
                new Player("13@MUCILON@34@4510@4832@4146@Bronze@52"),
                new Player("14@ZODXY@21@4554@5000@3940@Bronze@24"),
                new Player("15@SOL_CtblazeT@38@8856@13548@10038@Gold@39"),
                new Player("16@Citreaego Lemon@16@7913@8936@6744@Gold@3"),
                new Player("17@DEL_floating@34@2460@3609@2477@Rookie@28"),
                new Player("18@AIR-52Hz@25@6608@6878@4544@Silver@25"),
                new Player("19@Coach Kapreme@18@5375@8036@7495@Bronze@48"),
                new Player("20@Xmlczzzz@27@3063@4658@4213@Rookie@39"),
                new Player("21@hu_renn_son@35@6231@7475@7646@Silver@19"),
                new Player("22@SOL_Y1bo44@26@9909@14682@14631@Platinum@40"),
                new Player("23@xqr_flowersea@30@5242@4774@2356@Bronze@50"),
                new Player("24@Akuma@28@9030@6244@5330@Gold@30"),
                new Player("25@ch1kunkun@32@7971@10017@6215@Gold@16"),
                new Player("26@Jusna@37@5592@3980@3472@Bronze@53"),
                new Player("27@Qasver@24@5564@6217@6621@Bronze@32"),
                new Player("28@Atinum@35@9644@15328@16753@Platinum@37"),
                new Player("29@Rakkinishu@36@7697@9545@9573@Gold@49"),
                new Player("30@Robin@31@8149@8703@5305@Gold@40"),
                new Player("31@stanyyy@26@13905@22741@17499@Master@43"),
                new Player("32@Flyzz@33@8597@11502@8722@Gold@3"),
                new Player("33@COL Lux@29@5610@3965@2830@Bronze@18"),
                new Player("34@Lianr773@21@5125@5704@4798@Bronze@5"),
                new Player("35@xiaomo_B-zhan@24@5319@6047@4771@Bronze@44"),
                new Player("36@DontShotMyHead@19@2804@4423@4829@Rookie@25"),
                new Player("37@Ceez@28@5006@7013@5826@Bronze@36"),
                new Player("38@BBursty@37@9848@7608@5583@Platinum@56"),
                new Player("39@AUTOAIMER.EXE@25@4012@2915@1777@Bronze@41"),
                new Player("40@SRC CallSpades@29@12208@13898@6024@Diamond@44"),
                new Player("41@AXElA_sarahibo@22@2827@2130@1641@Rookie@58"),
                new Player("42@123t@17@5325@4998@4356@Bronze@4"),
                new Player("43@DYS_YuYu@20@5720@5243@3546@Silver@23"),
                new Player("44@sMaaaaack@19@10475@10747@3675@Platinum@54"),
                new Player("45@PE | Pigeon@33@8014@6092@6042@Gold@1"),
                new Player("46@Naghz@39@12219@14003@11286@Diamond@45"),
                new Player("47@kuadishangmen@23@6177@6978@6672@Silver@54"),
                new Player("48@TAITEI_o@25@7354@5456@3834@Silver@12"),
                new Player("49@BambinossTV@28@12473@16756@17367@Diamond@27"),
                new Player("50@SoEnvY@35@8335@8908@8591@Gold@19"),
                new Player("51@Skittlecakes@32@13317@19536@17289@Master@33"),
                new Player("52@Twitch aj_hk@29@7287@7333@4881@Silver@56"),
                new Player("53@ProteinVeins@27@5639@8939@8838@Bronze@54"),
                new Player("54@IcyRvR@27@9089@9439@7804@Gold@44"),
                new Player("55@WhatAWeek@28@10164@10260@8766@Platinum@42"),
                new Player("56@MDY_SnoopDogg@21@9209@8355@8100@Gold@12"),
                new Player("57@sakura52211@23@7821@5689@3482@Gold@27"),
                new Player("58@NM liemek@23@9123@11086@10046@Gold@59"),
                new Player("59@Air@26@10707@14328@8849@Platinum@47"),
                new Player("60@MaxMaiii@29@7307@5181@3368@Silver@24"),
                new Player("61@tat2ya@34@5023@5993@6179@Bronze@3"),
                new Player("62@jimmycooks@34@9758@12694@10831@Platinum@21"),
                new Player("63@PIKAYAYA@34@7262@10625@8745@Silver@51"),
                new Player("64@RexForeverX@21@4102@5637@3977@Bronze@2"),
                new Player("65@NRL_mErCy@34@7065@8850@6971@Silver@29"),
                new Player("66@Three_12321@17@9611@13100@12175@Platinum@8"),
                new Player("67@HexuFPS_TTV@35@3114@2793@2035@Rookie@28"),
                new Player("68@Revanixx@23@11612@17658@13547@Diamond@9"),
                new Player("69@erlin1111@20@9459@9889@10359@Gold@4"),
                new Player("70@hisofu@34@7641@10636@9550@Gold@38"),
                new Player("71@JDT_GURAN34@29@8494@10383@4643@Gold@19"),
                new Player("72@C9 rocker@16@16390@16912@14255@Predator@60"),
                new Player("73@PRM_Semp1ternal@27@2816@4354@4267@Rookie@29"),
                new Player("74@Kunther@38@14863@11099@3154@Master@59"),
                new Player("75@bilibilibandi@17@3261@3188@2223@Rookie@48"),
                new Player("76@RIG_@29@7764@11626@11996@Gold@38"),
                new Player("77@Karhu@32@8774@9529@9328@Gold@33"),
                new Player("78@Welfi@19@6608@4898@4671@Silver@8"),
                new Player("79@CapybaraParadis@36@4195@3091@2746@Bronze@55"),
                new Player("80@Prophet-RG@21@15961@24862@10156@Predator@60"),
                new Player("81@p4trick1994@28@6625@11047@9118@Silver@44"),
                new Player("82@ZETA_Yamazakin@18@6493@6965@4475@Silver@22"),
                new Player("83@voiid@19@6358@10641@8669@Silver@43"),
                new Player("84@spo_ody@39@5249@8481@7066@Bronze@20"),
                new Player("85@Rebyh@28@7356@9178@9024@Silver@11"),
                new Player("86@NM seven@23@5780@7334@5205@Silver@60"),
                new Player("87@INZI@27@7156@7693@5861@Silver@13"),
                new Player("88@Ret_BenTop@31@9740@11175@5045@Platinum@54"),
                new Player("89@ImNaoki@25@6494@5720@3650@Silver@18"),
                new Player("90@Fossildarth@30@8841@10382@9158@Gold@52"),
                new Player("91@Zerbow@33@7342@5257@3516@Silver@36"),
                new Player("92@Douyin_AsapnFeng@37@10327@8769@6799@Platinum@23"),
                new Player("93@Jonny@30@11406@7815@6597@Diamond@32"),
                new Player("94@kimosugi_oops@17@2957@2053@1930@Rookie@13"),
                new Player("95@FA_ReshEclipse@33@12162@15258@7212@Diamond@20"),
                new Player("96@RadenMas9@18@9169@14986@14873@Gold@42"),
                new Player("97@VJEIX@23@8712@9322@8164@Gold@32"),
                new Player("98@Rarelily7211514@31@7168@9799@5938@Silver@46"),
                new Player("99@BILI_Zmap@36@9024@7595@5724@Gold@18")
            };
            Legend[] legends = new Legend[]
            {
                new Legend("1@Crypto@Limelight"),
                new Legend("2@Mad Maggie@Midnight"),
                new Legend("3@Catalyst@Flamingo"),
                new Legend("4@Lifeline@Limelight"),
                new Legend("5@Caustic@Flamingo"),
                new Legend("6@Bloodhound@Rage"),
                new Legend("7@Wattson@Original"),
                new Legend("8@Ash@Hydro"),
                new Legend("9@Seer@Rage"),
                new Legend("10@Wraith@Hydro"),
                new Legend("11@Gibraltar@Limelight"),
                new Legend("12@Caustic@Rage"),
                new Legend("13@Mirage@Rage"),
                new Legend("14@Pathfinder@Midnight"),
                new Legend("15@Newcastle@Hydro"),
                new Legend("16@Wraith@Orchid"),
                new Legend("17@Bloodhound@Mandarin"),
                new Legend("18@Lifeline@Midnight"),
                new Legend("19@Mirage@Limelight"),
                new Legend("20@Revenant@Orchid"),
                new Legend("21@Pathfinder@Orchid"),
                new Legend("22@Gibraltar@Rage"),
                new Legend("23@Horizon@Orchid"),
                new Legend("24@Pathfinder@Mandarin"),
                new Legend("25@Seer@Midnight"),
                new Legend("26@Octane@Orchid"),
                new Legend("27@Fuse@Midnight"),
                new Legend("28@Pathfinder@Mandarin"),
                new Legend("29@Bloodhound@Flamingo"),
                new Legend("30@Wattson@Mandarin"),
                new Legend("31@Revenant@Flamingo"),
                new Legend("32@Rampart@Midnight"),
                new Legend("33@Newcastle@Flamingo"),
                new Legend("34@Loba@Midnight"),
                new Legend("35@Ash@Original"),
                new Legend("36@Lifeline@Rage"),
                new Legend("37@Loba@Rage"),
                new Legend("38@Mad Maggie@Hydro"),
                new Legend("39@Rampart@Limelight"),
                new Legend("40@Wraith@Original"),
                new Legend("41@Gibraltar@Orchid"),
                new Legend("42@Bangalore@Rage"),
                new Legend("43@Revenant@Limelight"),
                new Legend("44@Ash@Mandarin"),
                new Legend("45@Gibraltar@Mandarin"),
                new Legend("46@Caustic@Midnight"),
                new Legend("47@Octane@Mandarin"),
                new Legend("48@Loba@Orchid"),
                new Legend("49@Revenant@Original"),
                new Legend("50@Wattson@Original"),
                new Legend("51@Seer@Hydro"),
                new Legend("52@Gibraltar@Limelight"),
                new Legend("53@Rampart@Flamingo"),
                new Legend("54@Loba@Original"),
                new Legend("55@Wraith@Midnight"),
                new Legend("56@Revenant@Orchid"),
                new Legend("57@Pathfinder@Mandarin"),
                new Legend("58@Gibraltar@Mandarin"),
                new Legend("59@Mirage@Rage"),
                new Legend("60@Revenant@Orchid"),
            };
            Match[] matches = new Match[]
            {
                new Match("1@15.68@Ranked Arenas@Drop Off"),
                new Match("2@19.59@Arenas@Phase Runner"),
                new Match("3@20.5@Ranked Arenas@Drop Off"),
                new Match("4@17.86@Arenas@Rotating Map"),
                new Match("5@24.63@Arenas@Habitat 4"),
                new Match("6@16.45@Ranked Arenas@Phase Runner"),
                new Match("7@22.17@Ranked Arenas@Overflow"),
                new Match("8@20.52@Trios@Storm Point"),
                new Match("9@15.23@Ranked Leagues@Olympus"),
                new Match("10@24.6@Ranked Leagues@Kings Canyon"),
                new Match("11@18.61@Duos@World's Edge"),
                new Match("12@16.68@Duos@Storm Point"),
                new Match("13@16.43@Duos@Storm Point"),
                new Match("14@17.8@Trios@Kings Canyon"),
                new Match("15@19@Ranked Leagues@Storm Point"),
                new Match("16@22.73@Ranked Leagues@Storm Point"),
                new Match("17@22.5@Trios@Olympus"),
                new Match("18@17.94@Ranked Leagues@World's Edge"),
                new Match("19@20.93@Duos@Olympus")
            };
            EndGameStat[] stats = new EndGameStat[]
            {
                new EndGameStat("1@4@0.5@235@51@1"),
                new EndGameStat("2@3@1.5@455@56@1"),
                new EndGameStat("3@17@5@1346@40@1"),
                new EndGameStat("4@26@10.5@2283@1@1"),
                new EndGameStat("5@21@7@1742@91@1"),
                new EndGameStat("6@6@2@707@29@1"),
                new EndGameStat("7@23@9@1943@55@1"),
                new EndGameStat("8@22@6.5@1962@60@1"),
                new EndGameStat("9@15@6.5@1282@45@1"),
                new EndGameStat("10@16@6@1654@7@1"),
                new EndGameStat("11@13@5@1414@41@1"),
                new EndGameStat("12@25@9@2259@34@1"),
                new EndGameStat("13@26@8.5@2275@48@2"),
                new EndGameStat("14@7@2@525@21@2"),
                new EndGameStat("15@25@8@2004@84@2"),
                new EndGameStat("16@20@7@1748@18@2"),
                new EndGameStat("17@10@2.5@877@65@2"),
                new EndGameStat("18@21@8.5@1930@30@2"),
                new EndGameStat("19@12@3@830@16@2"),
                new EndGameStat("20@11@2@607@82@2"),
                new EndGameStat("21@23@7@2055@5@2"),
                new EndGameStat("22@30@9.5@2367@73@2"),
                new EndGameStat("23@1@1@194@46@2"),
                new EndGameStat("24@29@10.5@2648@19@2"),
                new EndGameStat("25@17@4.5@1402@59@2"),
                new EndGameStat("26@16@6@1823@39@3"),
                new EndGameStat("27@28@9.5@2373@27@3"),
                new EndGameStat("28@7@1@425@65@3"),
                new EndGameStat("29@29@8.5@2387@64@3"),
                new EndGameStat("30@30@11@2782@31@3"),
                new EndGameStat("31@4@1.5@489@73@3"),
                new EndGameStat("32@10@3.5@877@58@3"),
                new EndGameStat("33@18@6.5@1493@78@3"),
                new EndGameStat("34@20@6.5@1814@10@3"),
                new EndGameStat("35@17@5.5@1516@13@3"),
                new EndGameStat("36@1@1@583@30@3"),
                new EndGameStat("37@13@5.5@1323@24@3"),
                new EndGameStat("38@19@6@1671@49@3"),
                new EndGameStat("39@11@4@1186@89@3"),
                new EndGameStat("40@21@6@1516@61@3"),
                new EndGameStat("41@12@6.5@1413@99@3"),
                new EndGameStat("42@22@8@1877@21@3"),
                new EndGameStat("43@2@2@233@2@3"),
                new EndGameStat("44@6@2@524@90@3"),
                new EndGameStat("45@26@10.5@2469@36@4"),
                new EndGameStat("46@30@12.5@2867@31@4"),
                new EndGameStat("47@15@5@1113@34@4"),
                new EndGameStat("48@9@3@826@49@4"),
                new EndGameStat("49@14@6@1481@29@4"),
                new EndGameStat("50@7@3.5@641@33@4"),
                new EndGameStat("51@19@5.5@1504@52@4"),
                new EndGameStat("52@2@2.5@586@62@4"),
                new EndGameStat("53@28@10.5@2481@95@4"),
                new EndGameStat("54@1@2@389@88@5"),
                new EndGameStat("55@15@4@1250@6@5"),
                new EndGameStat("56@10@3.5@802@28@5"),
                new EndGameStat("57@6@2.5@508@34@5"),
                new EndGameStat("58@21@8.5@1785@98@5"),
                new EndGameStat("59@9@3.5@874@89@5"),
                new EndGameStat("60@8@3@876@67@5"),
                new EndGameStat("61@23@8.5@1992@13@6"),
                new EndGameStat("62@8@4@999@77@6"),
                new EndGameStat("63@13@3.5@1071@62@6"),
                new EndGameStat("64@18@4.5@1375@46@6"),
                new EndGameStat("65@30@9.5@2482@33@6"),
                new EndGameStat("66@24@6@1485@8@6"),
                new EndGameStat("67@20@6.5@1540@2@6"),
                new EndGameStat("68@9@4.5@870@80@6"),
                new EndGameStat("69@7@3@926@59@6"),
                new EndGameStat("70@10@3.5@1067@37@6"),
                new EndGameStat("71@27@10.5@2539@95@6"),
                new EndGameStat("72@1@-1.5@-106@85@6"),
                new EndGameStat("73@3@1@335@24@6"),
                new EndGameStat("74@5@1@445@99@6"),
                new EndGameStat("75@26@10@2441@1@6"),
                new EndGameStat("76@21@6@1826@87@6"),
                new EndGameStat("77@2@2.5@442@11@6"),
                new EndGameStat("78@23@8.5@2057@42@7"),
                new EndGameStat("79@15@5.5@1416@72@7"),
                new EndGameStat("80@9@2.5@723@35@7"),
                new EndGameStat("81@24@9@2251@31@7"),
                new EndGameStat("82@17@5.5@1544@74@7"),
                new EndGameStat("83@3@2@612@48@7"),
                new EndGameStat("84@26@11.5@2570@46@7"),
                new EndGameStat("85@28@11@2753@50@7"),
                new EndGameStat("86@12@6.5@1414@8@7"),
                new EndGameStat("87@16@6@1471@95@7"),
                new EndGameStat("88@8@1@439@47@7"),
                new EndGameStat("89@19@8@1867@53@7"),
                new EndGameStat("90@4@0.5@320@26@7"),
                new EndGameStat("91@19@7.5@1738@20@8"),
                new EndGameStat("92@4@3.5@832@77@8"),
                new EndGameStat("93@28@10.5@2775@17@8"),
                new EndGameStat("94@17@6.5@1484@80@8"),
                new EndGameStat("95@15@5@1436@29@8"),
                new EndGameStat("96@12@6.5@1282@83@8"),
                new EndGameStat("97@30@11@2685@70@8"),
                new EndGameStat("98@27@11.5@2680@97@8"),
                new EndGameStat("99@22@7.5@1643@28@8"),
                new EndGameStat("100@21@9@1969@81@8"),
                new EndGameStat("101@25@9.5@2138@9@8"),
                new EndGameStat("102@24@9@2274@1@8"),
                new EndGameStat("103@7@3@885@11@8"),
                new EndGameStat("104@5@2.5@361@93@8"),
                new EndGameStat("105@13@6@1296@6@8"),
                new EndGameStat("106@20@9@1985@30@8"),
                new EndGameStat("107@30@10.5@2839@70@9"),
                new EndGameStat("108@16@5.5@1261@47@9"),
                new EndGameStat("109@10@5@1189@88@9"),
                new EndGameStat("110@22@8.5@2072@69@9"),
                new EndGameStat("111@2@2@420@48@9"),
                new EndGameStat("112@3@1@125@28@9"),
                new EndGameStat("113@17@6.5@1312@68@9"),
                new EndGameStat("114@18@6@1318@20@9"),
                new EndGameStat("115@5@2@581@73@9"),
                new EndGameStat("116@4@-0@273@18@9"),
                new EndGameStat("117@20@7@1580@78@9"),
                new EndGameStat("118@13@5.5@1283@19@9"),
                new EndGameStat("119@14@5@1379@31@9"),
                new EndGameStat("120@28@9@2276@23@9"),
                new EndGameStat("121@21@6.5@1705@76@9"),
                new EndGameStat("122@9@4@746@91@10"),
                new EndGameStat("123@21@6@1524@64@10"),
                new EndGameStat("124@27@9.5@2245@46@10"),
                new EndGameStat("125@28@9@2117@5@10"),
                new EndGameStat("126@24@9.5@2546@97@10"),
                new EndGameStat("127@10@3.5@853@17@10"),
                new EndGameStat("128@8@2.5@706@2@10"),
                new EndGameStat("129@11@3.5@1191@19@10"),
                new EndGameStat("130@2@2@347@93@10"),
                new EndGameStat("131@14@6@1390@37@10"),
                new EndGameStat("132@23@8@2071@73@10"),
                new EndGameStat("133@19@5.5@1531@44@11"),
                new EndGameStat("134@1@1@126@38@11"),
                new EndGameStat("135@14@7@1450@19@11"),
                new EndGameStat("136@7@2@630@3@11"),
                new EndGameStat("137@18@6@1657@39@11"),
                new EndGameStat("138@4@2@601@43@11"),
                new EndGameStat("139@6@0.5@304@94@11"),
                new EndGameStat("140@22@6.5@1891@36@11"),
                new EndGameStat("141@29@9.5@2354@71@11"),
                new EndGameStat("142@24@8@1747@25@11"),
                new EndGameStat("143@13@5.5@1461@13@11"),
                new EndGameStat("144@9@4.5@1182@21@11"),
                new EndGameStat("145@28@9.5@2328@99@11"),
                new EndGameStat("146@5@0@302@8@11"),
                new EndGameStat("147@2@2.5@388@7@11"),
                new EndGameStat("148@20@6.5@1835@14@11"),
                new EndGameStat("149@25@8@2230@50@11"),
                new EndGameStat("150@8@1@410@53@11"),
                new EndGameStat("151@7@2.5@747@87@12"),
                new EndGameStat("152@30@10.5@2668@2@12"),
                new EndGameStat("153@26@9.5@2332@65@12"),
                new EndGameStat("154@8@2.5@880@67@12"),
                new EndGameStat("155@1@0.5@392@31@12"),
                new EndGameStat("156@6@2.5@831@56@12"),
                new EndGameStat("157@27@8@2234@55@12"),
                new EndGameStat("158@15@6@1235@94@12"),
                new EndGameStat("159@12@5.5@1430@20@12"),
                new EndGameStat("160@29@10@2369@83@12"),
                new EndGameStat("161@16@5.5@1340@63@12"),
                new EndGameStat("162@22@8.5@2017@30@12"),
                new EndGameStat("163@25@8.5@2167@69@12"),
                new EndGameStat("164@20@8.5@1973@19@12"),
                new EndGameStat("165@1@-1.5@-48@16@13"),
                new EndGameStat("166@20@5.5@1605@34@13"),
                new EndGameStat("167@24@7.5@2067@25@13"),
                new EndGameStat("168@9@2.5@912@49@13"),
                new EndGameStat("169@3@2@492@14@13"),
                new EndGameStat("170@16@7@1472@88@13"),
                new EndGameStat("171@5@0@80@86@13"),
                new EndGameStat("172@27@9.5@2411@50@13"),
                new EndGameStat("173@8@3.5@994@91@13"),
                new EndGameStat("174@12@5.5@1295@61@13"),
                new EndGameStat("175@7@4@794@2@13"),
                new EndGameStat("176@30@10@2419@62@13"),
                new EndGameStat("177@25@10.5@2351@66@13"),
                new EndGameStat("178@26@9@2342@56@13"),
                new EndGameStat("179@10@5.5@1177@26@13"),
                new EndGameStat("180@13@6.5@1289@26@14"),
                new EndGameStat("181@29@13@3067@62@14"),
                new EndGameStat("182@24@6.5@1915@55@14"),
                new EndGameStat("183@5@2@688@43@14"),
                new EndGameStat("184@2@0.5@307@1@14"),
                new EndGameStat("185@19@8.5@2029@29@14"),
                new EndGameStat("186@16@6@1621@63@14"),
                new EndGameStat("187@1@2.5@514@77@14"),
                new EndGameStat("188@17@6@1364@82@14"),
                new EndGameStat("189@27@11@2725@67@14"),
                new EndGameStat("190@3@2@606@7@14"),
                new EndGameStat("191@23@6.5@1825@12@14"),
                new EndGameStat("192@11@3@936@72@14"),
                new EndGameStat("193@6@2.5@646@49@14"),
                new EndGameStat("194@12@3.5@957@65@14"),
                new EndGameStat("195@15@4.5@1063@4@14"),
                new EndGameStat("196@28@9.5@2668@28@14"),
                new EndGameStat("197@26@9@2110@5@14"),
                new EndGameStat("198@7@5@923@51@14"),
                new EndGameStat("199@22@9@2325@23@14"),
                new EndGameStat("200@24@9@2376@19@15"),
                new EndGameStat("201@27@10@2671@34@15"),
                new EndGameStat("202@21@8.5@1976@92@15"),
                new EndGameStat("203@22@9.5@2076@33@15"),
                new EndGameStat("204@23@9@2221@74@15"),
                new EndGameStat("205@9@3@747@3@15"),
                new EndGameStat("206@1@0.5@461@27@15"),
                new EndGameStat("207@3@0.5@158@54@15"),
                new EndGameStat("208@11@4@979@2@15"),
                new EndGameStat("209@18@6.5@1876@31@15"),
                new EndGameStat("210@19@8@1779@56@16"),
                new EndGameStat("211@28@10@2511@71@16"),
                new EndGameStat("212@9@3.5@992@6@16"),
                new EndGameStat("213@26@10@2573@88@16"),
                new EndGameStat("214@10@4.5@1072@64@16"),
                new EndGameStat("215@18@7.5@1693@83@16"),
                new EndGameStat("216@17@6.5@1326@89@16"),
                new EndGameStat("217@25@10.5@2410@51@16"),
                new EndGameStat("218@7@4@868@28@16"),
                new EndGameStat("219@5@3@889@67@16"),
                new EndGameStat("220@21@7.5@1903@14@16"),
                new EndGameStat("221@15@5.5@1358@47@16"),
                new EndGameStat("222@20@8.5@2121@90@16"),
                new EndGameStat("223@1@1@462@99@16"),
                new EndGameStat("224@8@4@875@24@16"),
                new EndGameStat("225@4@3@606@65@16"),
                new EndGameStat("226@24@7.5@2141@42@17"),
                new EndGameStat("227@5@1.5@523@54@17"),
                new EndGameStat("228@23@9@2253@20@17"),
                new EndGameStat("229@30@10.5@2438@31@17"),
                new EndGameStat("230@9@3.5@685@93@17"),
                new EndGameStat("231@22@6.5@1892@78@17"),
                new EndGameStat("232@17@7@1649@90@17"),
                new EndGameStat("233@12@6.5@1420@13@17"),
                new EndGameStat("234@1@3@708@45@17"),
                new EndGameStat("235@25@11@2420@5@17"),
                new EndGameStat("236@2@1.5@200@46@17"),
                new EndGameStat("237@15@4.5@1244@88@17"),
                new EndGameStat("238@6@3.5@774@3@17"),
                new EndGameStat("239@3@1.5@355@32@17"),
                new EndGameStat("240@20@8@2011@77@17"),
                new EndGameStat("241@4@0.5@307@57@17"),
                new EndGameStat("242@19@9@2045@15@17"),
                new EndGameStat("243@12@3@1029@84@18"),
                new EndGameStat("244@11@5.5@1211@85@18"),
                new EndGameStat("245@24@8@1810@59@18"),
                new EndGameStat("246@3@2@231@7@18"),
                new EndGameStat("247@18@5.5@1685@33@18"),
                new EndGameStat("248@16@6.5@1588@83@18"),
                new EndGameStat("249@15@4@1134@52@18"),
                new EndGameStat("250@27@9.5@2417@81@18"),
                new EndGameStat("251@9@3.5@856@5@18"),
                new EndGameStat("252@10@5@1037@94@18"),
                new EndGameStat("253@1@2@446@54@18"),
                new EndGameStat("254@2@1@289@64@18"),
                new EndGameStat("255@8@2@620@43@18"),
                new EndGameStat("256@25@9@2392@55@18"),
                new EndGameStat("257@2@-0.5@104@85@19"),
                new EndGameStat("258@19@7@1823@95@19"),
                new EndGameStat("259@21@6.5@1812@1@19"),
                new EndGameStat("260@20@7.5@2106@32@19"),
                new EndGameStat("261@16@7.5@1660@99@19"),
                new EndGameStat("262@30@11.5@3048@52@19"),
                new EndGameStat("263@9@4@767@89@19"),
                new EndGameStat("264@24@9.5@2051@79@19"),
                new EndGameStat("265@10@5@1019@35@19"),
                new EndGameStat("266@27@9@2487@2@19"),
                new EndGameStat("267@17@8@1665@11@19"),
                new EndGameStat("268@25@8.5@2173@44@19")
            };

            modelBuilder.Entity<Player>().HasData(players);
            modelBuilder.Entity<Legend>().HasData(legends);
            modelBuilder.Entity<Match>().HasData(matches);
            modelBuilder.Entity<EndGameStat>().HasData(stats);
        }

        // Tables
        public DbSet<EndGameStat> Stats { get; set; }
        public DbSet<Legend> Legends { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
