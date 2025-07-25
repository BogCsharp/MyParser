﻿using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Core
{
    internal class ParserWorker<T> where T : class
    {
        IParser<T> parser;

        IParserSettings parserSettings;
        HtmlLoader htmlLoader;
        bool  isActive;

        public event Action<object,T> OnNewData;
        public event Action<object> OnCompleted;
        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }
        public IParserSettings Settings
        {
            get { return parserSettings; }
            set { parserSettings = value; htmlLoader = new HtmlLoader(value); }
        }

        public bool IsActive
        {
            get { return isActive; }
        }
     


        public ParserWorker(IParser<T> parser)
        {
            this.parser= parser;
        }
        public ParserWorker(IParser<T> parser, IParserSettings parserSettings):this(parser) 

        {
                this.parserSettings = parserSettings;

        }
        public void Start()
        {
            isActive = true;
            Work();
        }
        public void Stop()
        {
            isActive = false;
            

        }
        private async void Work()
        {
            for(int i= parserSettings.StartPoint; i<= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }
                var source= await htmlLoader.GetSourceByPageId(i);
                var domParser = new HtmlParser();
                var document=await domParser.ParseDocumentAsync(source);

                var result=parser.Parse(document);

                OnNewData?.Invoke(this,result);
                
            }
            OnCompleted?.Invoke(this);
            isActive = false;
        }

        
    }
}
