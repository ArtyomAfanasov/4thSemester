/// Содержит функцию, принимающую адрес веб-страницы, скачивающую все веб-страницы, на которые есть ссылки с указанной, 
/// и печатающую информацию о размере каждой в формате "адрес страницы --- число символов". Ссылки нужно обрабатывать только 
/// заданные в форме <a href="http://...">.
module WebPagesDownloader
    
    open System.Text.RegularExpressions
    open System.Net
    open System.IO
    open System.Collections.Generic

    /// Загрузчик информации о внутренних веб-страницах указанной страницы.
    type WebPagesDownloader() = 
        /// Регулярное выражение для поиска ссылок на указанной странице.
        let findInnerWebPages = new Regex(@"a href=""http://(\S*)""")
        
        /// Нормализовать найденный шаблон `a href="http://..."` до `http://...`
        let normalizeUrl = new Regex(@"a href=|""")
    
        /// Скачать html.
        let downloadHtml url = 
            async {
                do printfn "Creating request for %s..." url
                let request = WebRequest.Create(url)
                try
                    use! response = request.AsyncGetResponse() 
                    do printfn "Getting response stream for %s..." url
                    use stream = response.GetResponseStream()
                    do printfn "Reading response for %s..." url
                    use reader = new StreamReader(stream)
                    return reader.ReadToEnd()
                with 
                | :? System.Net.WebException -> 
                    return url + " not found."
                | :? System.Exception as e -> 
                    do printfn "%s" e.Message
                    return e.Message
            }

        /// Применить регулярное выражение к html-коду страницы.
        let matchHtml html (pagesList : List<string>) = 
            let matches = findInnerWebPages.Matches(html)
            for match' in matches do
                if match'.Success then 
                    let captures = match'.Captures
                    for capture in captures do   
                        pagesList.Add (normalizeUrl.Replace(capture.Value, System.String.Empty))
                else printfn "No internal pages here."    
            pagesList

        /// Вывести данные о внутренних страницах.
        let printSize pagesList = 
            pagesList
            |> Seq.map (fun url -> 
                async {
                    let! html = downloadHtml url
                    match html with
                    | x when x = url + " not found" -> printfn "%s not found" url
                    | _ -> 
                        do printfn "%s --- %i" url html.Length
                })
            |> Async.Parallel            
            |> Async.RunSynchronously
            |> ignore

        /// Вывести все пары `(адрес страницы * число символов)` для веб-страниц, указанных в данной.
        let printSizeOfAllInnerWebPages url =
            async {
                let! mainHtml = downloadHtml url
                match mainHtml with
                | x when x = url + " not found" -> printfn "%s not found" url
                | _ ->            
                    let innerPages = matchHtml mainHtml (new List<string>())
    
                    do printfn "Print couple `ulr * amount of characters` for inner web pages..."
            
                    do printSize innerPages
    
                    do printfn "That's all."
            }

        /// Вывести все пары `(адрес страницы * число символов)` для веб-страниц, указанных в данной.
        member this.PrintSizeOfAllInnerWebPages url = printSizeOfAllInnerWebPages url

    [<EntryPoint>]
    let main arg =  
        let cs = WebPagesDownloader()
        cs.PrintSizeOfAllInnerWebPages @"http://se.math.spbu.ru/SE"
        |> Async.RunSynchronously
        |> ignore
        0