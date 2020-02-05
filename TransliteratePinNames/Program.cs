using ConnectToE3;
using e3;
using System;
using System.Diagnostics;
using UnidecodeSharpFork;

namespace TransliterateTextNames
{
    class Program
    {
        private static AppConnect e3App = new AppConnect();
        public static e3Application App; // объект приложения
        public static e3Job Prj = null; // объект проекта
        public static e3Symbol Sym = null;	// объект 
        public static e3Text text = null;	// объект 

        static void Main()
        {
            // Объекты массивов Id
            Object symIds = new Object();
            Object textIds = new Object();

            // Подключаем E3
            App = e3App.ToE3();
            App?.PutInfo(0, "Starting Transliterating!");
            Prj = App?.CreateJobObject();
            Sym = Prj.CreateSymbolObject();
            text = Prj.CreateTextObject();
            // Получаем массив Id символов
            Prj.GetSymbolIds(ref symIds);


            foreach (var symId in (Array)symIds)
            {
                if (symId != null)
                {
                    Sym.SetId((int)symId);
                    // Получаем массив Id текстов типа "12"
                    Sym.GetTextIds(ref textIds, 12);
                    foreach (var textId in (Array)textIds)
                    {
                        if (textId != null)
                        {
                            text.SetId((int)textId);

                            if (text.GetText() != "")
                            {
                                Debug.WriteLine($"Type - {text.GetTypeId()}");
                                Debug.Indent();
                                Debug.WriteLine(text.GetText());
                                // Заменяем текст на транслитерированный библиотекой UnidecodeSharpFork
                                text.SetText(text.GetText().Unidecode());
                                Debug.WriteLine(text.GetText());
                                Debug.Unindent();
                            }
                        }
                    }

                }
            }
            // Подвисает взаимодействие с символами в E3
            e3App = null;
            App = null;          

            Console.ReadLine();
        }
    }
}
