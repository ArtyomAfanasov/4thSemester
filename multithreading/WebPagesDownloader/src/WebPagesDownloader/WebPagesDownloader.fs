/// Содержит функцию, принимающую адрес веб-страницы, скачивающую все веб-страницы, на которые есть ссылки с указанной, 
/// и печатающую информацию о размере каждой в формате "адрес страницы --- число символов". Ссылки нужно обрабатывать только 
/// заданные в форме <a href="http://...">.
module WebPagesDownloader
    
    open System.Text.RegularExpressions
    open System.Net
    open System.IO
    open System.Collections.Generic

    /// Регулярное выражение для поиска ссылок на указанной странице.
    let findInnerWebPages = new Regex(@"<a href=""http://(\w*)"">")

    ///// Скачать html.
    //let downloadHtml url = 
    //    async {
    //        do printfn "Creating request for %s..." url
    //        let request = WebRequest.Create(url)
    //        use! response = request.AsyncGetResponse()
    //        do printfn "Getting response stream for %s..." url
    //        use stream = response.GetResponseStream()
    //        do printfn "Reading response for %s..." url
    //        use reader = new StreamReader(stream)
    //        return reader.ReadToEnd()
    //    }
    //
    ///// Вывести все пары `(адрес страницы * число символов)` для веб-страниц, указанных в данной.
    //let printSizeOfAllInnerWebPages url =
    //    async {
    //        let! mainHtml = downloadHtml url
    //        
    //        let innerPages = new List<string>()
    //
    //        let matches = findInnerWebPages.Matches(mainHtml)
    //        for match' in matches do
    //            if match'.Success then 
    //                let captures = match'.Captures
    //                for capture in captures do   
    //                    innerPages.Add capture.Value
    //            else printfn "Внутренних страниц нет."    
    //
    //        //let countOfInnerPages = findInnerWebPages.Matches(mainHtml).Count
    //        //let mutable innerPages = Array.zeroCreate<string> countOfInnerPages
    //        //findInnerWebPages.Matches(mainHtml).CopyTo(innerPages, 0)
    //
    //        do printfn "Print couple `ulr * amount of characters` for inner web pages..."
    //        
    //        innerPages
    //        |> Seq.map (fun url -> 
    //            async {
    //                let! html = downloadHtml url
    //                (printfn "%s --- %s" url, html.Length) 
    //                |> ignore
    //            })
    //        |> Async.Parallel
    //        |> Async.RunSynchronously
    //        |> ignore
    //
    //        printfn "It's end."
    //    }
    //    |> ignore                       
    
    /// Вывести все пары `(адрес страницы * число символов)` для веб-страниц, указанных в данной.
    let printSizeOfAllInnerWebPages url =
        async {
            do printfn "Creating request for %s..." url
            let request = WebRequest.Create(url)
            use! response = request.AsyncGetResponse()
            do printfn "Getting response stream for %s..." url
            use stream = response.GetResponseStream()
            do printfn "Reading response for %s..." url
            use reader = new StreamReader(stream)
            let mainHtml = reader.ReadToEnd()            
            
            let innerPages = new List<string>()

            let matches = findInnerWebPages.Matches(mainHtml)
            for match' in matches do
                if match'.Success then 
                    let captures = match'.Captures
                    for capture in captures do   
                        innerPages.Add capture.Value
                else printfn "Внутренних страниц нет."    

            //let countOfInnerPages = findInnerWebPages.Matches(mainHtml).Count
            //let mutable innerPages = Array.zeroCreate<string> countOfInnerPages
            //findInnerWebPages.Matches(mainHtml).CopyTo(innerPages, 0)

            do printfn "Print couple `ulr * amount of characters` for inner web pages..."
            
            innerPages
            |> Seq.map (fun url -> 
                async {
                    do printfn "Creating request for %s..." url
                    let request = WebRequest.Create(url)
                    use! response = request.AsyncGetResponse()
                    do printfn "Getting response stream for %s..." url
                    use stream = response.GetResponseStream()
                    do printfn "Reading response for %s..." url
                    use reader = new StreamReader(stream)
                    let html = reader.ReadToEnd()   
                    (printfn "%s --- %s" url, html.Length) 
                    |> ignore
                })
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore

            printfn "It's end."
        }
        |> ignore        

    [<EntryPoint>]
    let main arg = 
        //let regex = new Regex(@"<a href=""http://(\w*)"">")
        //let matches = regex.Matches(@"adasdawe21e21easd <a href=""http://ad12eas2""> asd21e211da <a href=""http://ad1213asd2eas2"">")
        //for match' in matches do
        //    if match'.Success then 
        //        //let groups = match'.Groups
        //        let captures = match'.Captures
        //        let countOfInnerPages = captures.Count
        //        let mutable innerPages = Array.zeroCreate<string> countOfInnerPages
        //        for capture in captures do   
        //            printfn "%s" capture.Value
        //    else printfn "sorry"    
        
        // let matches = findInnerWebPages.Matches(@"adasdawe21e21easd <a href=""http://se.math.spbu.ru""> asd21e211da <a href=""http://spisok.math.spbu.ru"">")
        //
        ///////////////let innerPages = new List<string>()
        ///////////////let matches = findInnerWebPages.Matches(@"adasdawe21e21easd <a href=""htasd213""> asd21e211da <a href=""http://se.math.spbu.ru"">")
        ///////////////for match' in matches do
        ///////////////    if match'.Success then 
        ///////////////        let captures = match'.Captures
        ///////////////        for capture in captures do   
        ///////////////            innerPages.Add capture.Value
        ///////////////    else printfn "Внутренних страниц нет."    
        ///////////////
        ///////////////innerPages
        ///////////////|> Seq.map (fun url -> 
        ///////////////    async {
        ///////////////        let! html = downloadHtml url
        ///////////////        (printfn "%s --- %s" url, html.Length) 
        ///////////////        |> ignore
        ///////////////    })
        ///////////////|> Async.Parallel
        ///////////////|> Async.RunSynchronously
        ///////////////|> ignore
        //printSizeOfAllInnerWebPages @"http://se.math.spbu.ru"       
        //System.Console.ReadLine() |> ignore

        printSizeOfAllInnerWebPages @"http://se.math.spbu.ru"

        0