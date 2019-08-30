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
                try                
                    let request = WebRequest.Create(url)
                    use! response = request.AsyncGetResponse() 
                    do printfn "Getting response stream for %s..." url
                    use stream = response.GetResponseStream()
                    do printfn "Reading response for %s..." url
                    use reader = new StreamReader(stream)
                    return reader.ReadToEnd()
                with 
                | :? System.Security.SecurityException as e ->
                    printfn "%s" e.Message
                    return "-1"
                | :? System.Net.WebException as e -> 
                    printfn "%s" e.Message
                    return "-1"
                | :? System.NotSupportedException as e ->
                    printfn "%s" e.Message
                    return "-1"
                | :? System.ArgumentNullException as e -> 
                    printfn "%s" e.Message
                    return "-1"
                | :? System.ArgumentException as e ->
                    printfn "%s" e.Message
                    return "-1"
                | :? System.OutOfMemoryException as e ->
                    printfn "%s" e.Message
                    return "-1"
                | :? System.IO.IOException as e ->
                    printfn "%s" e.Message
                    return "-1"                
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
                do printfn "Let's do this with %s!" url
                let! mainHtml = downloadHtml url
                match mainHtml with
                | "-1" -> printfn "Exit."
                | _ ->            
                    let innerPages = matchHtml mainHtml (new List<string>())
    
                    do printfn "Print couple `ulr * amount of characters` for inner web pages..."
            
                    do printSize innerPages
    
                    do printfn "That's all."
            }

        /// Вывести сообщение об ошибке.
        let printError message = 
            async {
                do printfn message
            }   
        
        /// Вывести все пары `(адрес страницы * число символов)` для веб-страниц, указанных в данной.
        member this.PrintSizeOfAllInnerWebPages url = 
            if url = null then 
                printError "Null is not allow."                
            elif System.Uri.IsWellFormedUriString(url, System.UriKind.RelativeOrAbsolute) <> true then
                printError "Uri is invalid."         
            else printSizeOfAllInnerWebPages url

    [<EntryPoint>]
    let main arg =          
        0