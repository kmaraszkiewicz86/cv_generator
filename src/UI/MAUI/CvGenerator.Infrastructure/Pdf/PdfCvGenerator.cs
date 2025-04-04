using CvGenerator.Domain.Models;
using CvGenerator.Domain.Pdf;
using CvGenerator.Infrastructure.Extensions;
using CvGenerator.Shared.Enums;
using CvGenerator.Shared.Pdf.Properties;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CvGenerator.Infrastructure.Pdf
{
    public class PdfCvGenerator : IPdfCvGenerator
    {
        public async Task<byte[]> CreatePdfAsync(ApplicationLanguageType type, 
            BasicPersonalDataResponse basicPersonalDataResponse,
            ResumeResponse resumeResponse,
            ContactResponse contactResponse)
        {
            byte[] mainPhotoBytes = await GetMainPhotoBase64StringAsync();

            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    PageMainStyle(page);

                    page
                    .Content()
                    .Padding(5, Unit.Millimetre)
                    .Row(row =>
                    {
                        row.ConstantItem(200)
                            .Background("#ff8502")
                            .Padding(2)
                            .Column(column =>
                            {
                                column
                                    .Item()
                                    .Image(mainPhotoBytes);

                                GeneratePersonalInfoSection(column, contactResponse, type);
                                GenerateSkillsSection(column, resumeResponse, type);
                                GenerateLanguagesSection(column, resumeResponse, type);
                            });

                        row.RelativeItem()
                            .Background("#555")
                            .PaddingLeft(10)
                            .Column(column =>
                            {
                                column
                                    .Item()
                                    .PaddingBottom(10)
                                    .Text(basicPersonalDataResponse.Name)
                                    .ExtraBold()
                                    .FontColor(CustomColors.RightPaneFontColor)
                                    .FontSize(CustomFontSize.MainTitleFontSize);

                                GenerateMainDescriptionSection(column, basicPersonalDataResponse);
                                GenerateEducationSection(column, resumeResponse, type);
                                GenerateWorkHistorySection(column, resumeResponse, type);
                                GenerateProjectsSection(column, resumeResponse, type);
                                GenerateCvClauseSection(column, type);
                            });

                    });

                    GenerateFooterSection(page);
                });
            })
            .GeneratePdf();
        }

        private static void PageMainStyle(PageDescriptor page)
        {
            page.Size(PageSizes.A4);
            page.PageColor(CustomColors.RightRowBackgroundColor);
            page.DefaultTextStyle(x => x.FontSize(CustomFontSize.NormalFontSize));
        }

        private static void GeneratePersonalInfoSection(ColumnDescriptor column, ContactResponse contactResponse, ApplicationLanguageType type)
        {
            AddTitleTextForLeftPanel(column, GetTranslatedWord(type, "Personal Info", "Informacje ogólne"));

            column
                .Item()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);
                        columns.RelativeColumn();
                    });

                    AddTableTitleColumnForLeftPanel(table, 1, 1, "Email:");
                    AddTableValueColumnForLeftPanel(table, 1, 2, contactResponse.Email);

                    AddTableTitleColumnForLeftPanel(table, 2, 1,
                        GetTranslatedWord(type, "Phone", "Telefon"));

                    AddTableValueColumnForLeftPanel(table, 2, 2, contactResponse.Phone);

                    AddTableTitleColumnForLeftPanel(table, 3, 1, GetTranslatedWord(type, "Address", "Adres"));
                    AddTableValueColumnForLeftPanel(table, 3, 2, $"{contactResponse.Adress}, {contactResponse.Residance}");

                    AddTableTitleColumnForLeftPanel(table, 4, 1, "Github:");
                    AddTableValueColumnForLeftPanel(table, 4, 2, contactResponse.Github);

                    AddTableTitleColumnForLeftPanel(table, 5, 1, "LinkedIn:");
                    AddTableValueColumnForLeftPanel(table, 5, 2, contactResponse.LinkedIn);
                });
        }

        private static void GenerateSkillsSection(ColumnDescriptor column, ResumeResponse resumeResponse, ApplicationLanguageType type)
        {
            if (!resumeResponse.Skills.Any())
            {
                return;
            }

            AddTitleTextForLeftPanel(column, GetTranslatedWord(type, "Skills", "Umiejętności"));

            foreach (SkillResponse skill in resumeResponse.Skills)
            {
                AddTextForLeftPanel(column, skill.Name);
            }
        }

        private static void GenerateLanguagesSection(ColumnDescriptor column, ResumeResponse resume, ApplicationLanguageType type)
        {
            AddTitleTextForLeftPanel(column, GetTranslatedWord(type, "Languages", "Języki"));

            uint rowIndex = 1;

            column
                .Item()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);
                        columns.RelativeColumn();
                    });

                    foreach (LanguageResponse language in resume.Languages)
                    {
                        AddTableTitleColumnForLeftPanel(table, rowIndex, 1, $"{language.Name}: ");
                        AddTableTitleColumnForLeftPanel(table, rowIndex++, 2, language.Level);
                    }
                });

            column.Item().PageBreak();
        }

        private static void GenerateMainDescriptionSection(ColumnDescriptor column, BasicPersonalDataResponse basicPersonalDataResponse)
        {
            foreach (var line in basicPersonalDataResponse.PersonalDataDescriptionLines)
            {
                column
                    .Item()
                    .PaddingBottom(3)
                    .Text(line.Line)
                    .FontColor(CustomColors.RightPaneFontColor);
            }
        }

        private static void GenerateEducationSection(ColumnDescriptor column, ResumeResponse resumeResponse, ApplicationLanguageType type)
        {
            AddTitleTextForRightPanel(column, GetTranslatedWord(type, "Education", "Edukacja"));

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(3);
                });

                uint rowIndex = 1;
                foreach (EducationResponse education in resumeResponse.Educations)
                {
                    GenerateEductionItemSection(table, education, rowIndex++, type);
                }
            });
        }

        private static void GenerateEductionItemSection(TableDescriptor table, EducationResponse education, uint rowIndex, ApplicationLanguageType type)
        {
            table.Cell()
                .Row(rowIndex)
                .Column(1)
                .Text($"{education.StartYear} - {(education.EndYear.HasValue ? education.EndYear : GetTranslatedWord(type, "Present", "Obecnie"))}")
                .FontColor(CustomColors.RightPaneFontColor);

            uint contentIndex = 1;
            table.Cell()
                .Row(rowIndex)
                .Column(2)
                .Table(resumeTable =>
                {
                    resumeTable.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                    });

                    resumeTable.Cell()
                        .Row(contentIndex++)
                        .Column(1)
                        .PaddingBottom(2)
                        .Text(education.SpecializationName)
                        .ExtraBold()
                        .FontColor(CustomColors.RightPaneFontColor);

                    resumeTable.Cell()
                        .Row(contentIndex++)
                        .Column(1)
                        .PaddingBottom(2)
                        .Text(education.SchoolName)
                        .NormalWeight()
                        .FontColor(CustomColors.RightPaneFontColor);

                    resumeTable.Cell()
                        .Row(contentIndex)
                        .Column(1)
                        .PaddingBottom(10)
                        .Text(education.EducationDescription)
                        .NormalWeight()
                        .FontColor(CustomColors.RightPaneFontColor);

                });
        }

        private static void GenerateProjectsSection(ColumnDescriptor column, ResumeResponse resumeResponse, ApplicationLanguageType type)
        {
            AddTitleTextForRightPanel(column, GetTranslatedWord(type, "Projects", "Projekty"));

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(3);
                });

                uint rowIndex = 1;
                foreach (ProjectResponse project in resumeResponse.Projects)
                {
                    GenerateProjectSection(table, project, rowIndex++);
                }
            });
        }

        private static void GenerateProjectSection(TableDescriptor table, ProjectResponse project, uint rowIndex)
        {
            table
                .Cell()
                .Row(rowIndex)
                .Column(1)
                .Text($"{project.StartDate.ToCustomString()} - {project.EndDate.ToCustomString()}")
                .FontColor(CustomColors.RightPaneFontColor);

            uint columnRowIndex = 1;
            table
            .Cell()
            .Row(rowIndex)
            .Column(2)
            .Table(columnTable =>
            {
                columnTable.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);
                });

                columnTable
                    .Cell()
                    .Row(columnRowIndex++)
                    .Column(1)
                    .Text(project.Name)
                    .ExtraBold()
                    .FontColor(CustomColors.RightPaneFontColor);

                GenerateDescriptionForProjectItem(columnTable, project, columnRowIndex++);

                columnTable
                    .Cell()
                    .Row(columnRowIndex++)
                    .Column(1)
                    .Padding(5)
                    .PaddingBottom(5)
                    .Text(project.SkillsUsed)
                    .Italic()
                    .NormalWeight()
                    .FontColor(CustomColors.RightPaneFontColor);
            });
        }

        private static void GenerateDescriptionForProjectItem(TableDescriptor table, ProjectResponse project, uint columnRowIndex)
        {
            table
                .Cell()
                .Row(columnRowIndex)
                .Column(1)
                .Table(descriptionTable =>
                {
                    descriptionTable.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                    });

                    uint lineRowIndex = 1;
                    foreach (ProjectDescriptionLineResponse line in project.DescriptionLines)
                    {
                        GenerateDecriptionLineItem(descriptionTable, line.Line, lineRowIndex++);
                    }
                });
        }


        private static void GenerateWorkHistorySection(ColumnDescriptor column, ResumeResponse resumeResponse, ApplicationLanguageType type)
        {
            AddTitleTextForRightPanel(column, GetTranslatedWord(type, "Work History", "Doświadczenie zawodowe"));

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(3);
                });

                uint rowIndex = 1;
                foreach (ExpirienceResponse expirience in resumeResponse.Expiriences)
                {
                    GenerateWorkHistoryItemSection(table, expirience, rowIndex++);
                }
            });
        }

        private static void GenerateWorkHistoryItemSection(TableDescriptor table, ExpirienceResponse expirience, uint rowIndex)
        {
            table
                .Cell()
                .Row(rowIndex)
                .Column(1)
                .Text($"{expirience.StartDate.ToCustomString()} - {expirience.EndDate.ToCustomString()}")
                .FontColor(CustomColors.RightPaneFontColor);

            uint columnRowIndex = 1;
            table
                .Cell()
                .Row(rowIndex)
                .Column(2)
                .Table(columnTable =>
                {
                    columnTable.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                    });

                    columnTable
                        .Cell()
                        .Row(columnRowIndex++)
                        .Column(1)
                        .Text(expirience.Position)
                        .ExtraBold()
                        .FontColor(CustomColors.RightPaneFontColor);

                    columnTable
                        .Cell()
                        .Row(columnRowIndex++)
                        .Column(1)
                        .Padding(5)
                        .Text(expirience.CompanyName)
                        .Italic()
                        .NormalWeight()
                        .FontColor(CustomColors.RightPaneFontColor);

                    GenerateDescriptionForWorkHistoryItem(columnTable, expirience, columnRowIndex++);

                    columnTable
                        .Cell()
                        .Row(columnRowIndex)
                        .Column(1)
                        .PaddingLeft(15)
                        .PaddingTop(2)
                        .Text($"* {expirience.UsedSkills}")
                        .Italic()
                        .NormalWeight()
                        .FontColor(CustomColors.RightPaneFontColor);

                });
        }

        private static void GenerateDescriptionForWorkHistoryItem(TableDescriptor table, ExpirienceResponse expirience, uint columnRowIndex)
        {
            table
                .Cell()
                .Row(columnRowIndex)
                .Column(1)
                .Table(descriptionTable =>
                {
                    descriptionTable.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                    });

                    uint lineRowIndex = 1;
                    foreach (ExpirienceDescriptionLineResponse line in expirience.ExpirienceDescriptionLines)
                    {
                        GenerateDecriptionLineItem(descriptionTable, line.Line, lineRowIndex++);
                    }
                });
        }

        private static void GenerateDecriptionLineItem(TableDescriptor table, string text, uint lineIndex)
        {
            table
                .Cell()
                .Row(lineIndex)
                .Column(1)
                .PaddingLeft(15)
                .PaddingBottom(5)
                .Text($"* {text}")
                .NormalWeight()
                .FontColor(CustomColors.RightPaneFontColor);
        }

        private static void GenerateCvClauseSection(ColumnDescriptor column, ApplicationLanguageType type)
        {
            var cvClause = "I hereby give consent for my personal data included in my application to be processed for the purposes of the recruitment process under the " +
                    "European Parliament's and Council of the European Union Regulation on the Protection of Natural Persons as of 27 April 2016, with regard " +
                    "to the processing of personal data and on the free movement of such data, and repealing Directive 95/46/EC (Data Protection Directive)";
            if (type == ApplicationLanguageType.PL)
            {
                cvClause = "Wyrażam zgodę na przetwarzanie podanych przeze mnie moich danych osobowych, których podanie nie wynika z przepisów prawa, " +
                    "przez PKP Polskie Linie Kolejowe Spółka Akcyjna z siedzibą znajdującą się pod adresem: 03-734 Warszawa, ul. Targowa 74, " +
                    "w celu rozpatrzenia mojej kandydatury na potrzeby niniejszej rekrutacji.";
            }

            uint paddingTop = type == ApplicationLanguageType.En
                               ? (uint)100
                               : 10;

            column
                .Item()
                .PaddingTop(paddingTop)
                .Text(cvClause)
                .FontSize(8)
                .FontColor(CustomColors.RightPaneFontColor);
        }

        private static void GenerateFooterSection(PageDescriptor page)
        {
            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
        }

        private static async Task<byte[]> GetMainPhotoBase64StringAsync()
        {
            string mainPhotoBase64String = await File.ReadAllTextAsync("Files/MainPhotoBase64String.txt");

            return Convert.FromBase64String(mainPhotoBase64String);
        }

        private static void AddTableTitleColumnForLeftPanel(TableDescriptor table, uint rowIndex, uint columnIndex, string text)
        {
            table.Cell()
                .Row(rowIndex)
                .Column(columnIndex)
                .PaddingLeft(10)
                .Text(text)
                .FontSize(CustomFontSize.NormalFontSize)
                .FontColor(CustomColors.LeftPaneFontColor);
        }

        private static void AddTableValueColumnForLeftPanel(TableDescriptor table, uint rowIndex, uint columnIndex, string text)
        {
            table.Cell()
                .Row(rowIndex)
                .Column(columnIndex)
                .PaddingLeft(10)
                .PaddingRight(3)
                .AlignRight()
                .Text(text)
                .FontSize(CustomFontSize.NormalFontSize)
                .FontColor(CustomColors.LeftPaneFontColor);
        }

        private static void AddTitleTextForRightPanel(ColumnDescriptor column, string text)
        {
            column
                .Item()
                .PaddingTop(5)
                .PaddingBottom(10)
                .Text(text)
                .Underline()
                .ExtraBold()
                .FontSize(CustomFontSize.TitleFontSize)
                .FontColor(CustomColors.RightTitleFontColor);
        }

        private static void AddTitleTextForLeftPanel(ColumnDescriptor column, string text)
        {
            column
                .Item()
                .PaddingLeft(3)
                .PaddingTop(10)
                .PaddingBottom(10)
                .Text(text)
                .Underline()
                .ExtraBold()
                .FontSize(CustomFontSize.TitleFontSize)
                .FontColor(CustomColors.LeftPaneFontColor);
        }

        private static void AddTextForLeftPanel(ColumnDescriptor column, string text)
        {
            column
                .Item()
                .PaddingLeft(15)
                .PaddingBottom(5)
                .Text(text)
                .FontSize(CustomFontSize.NormalFontSize)
                .FontColor(CustomColors.LeftPaneFontColor);
        }

        private static string GetTranslatedWord(ApplicationLanguageType type, string englishWord, string polishWord)
            => type switch
            {
                ApplicationLanguageType.En => englishWord,
                _ => polishWord,
            };
    }
}
