using System.IO;
using System.Linq;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace DocumentPlagiarismChecker.Comparers.WordCounter
{
    internal class Document: Core.BaseDocument
    {        
        public Dictionary<string, int> WordAppearances {get; set;}
        

        /// <summary>
        /// Loads the content of a PDF file and counts how many words and how many times appears along the document.
        /// </summary>
        /// <param name="path">The file path.</param>
        public Document(string path): base(path){
            //Check pre-conditions        
            if(!System.IO.Path.GetExtension(path).ToLower().Equals(".pdf"))
                throw new FileNotPdfException();

            //Init object attributes.
            WordAppearances = new Dictionary<string, int>();

            //Read PDF file and sotre the word count.
            using (PdfReader reader = new PdfReader(path))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string text = PdfTextExtractor.GetTextFromPage(reader, i);
                    text = text.Replace("\n", "");

                    foreach(string word in text.Split(" ").Where(x => x.Length > 0)){
                        if(!WordAppearances.ContainsKey(word))
                            WordAppearances.Add(word, 0);
                                    
                        WordAppearances[word]++;     
                    }
                }
            }            
        }
    }
}