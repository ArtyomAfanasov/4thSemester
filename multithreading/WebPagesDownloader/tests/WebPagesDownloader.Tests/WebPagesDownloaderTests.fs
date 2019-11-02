namespace WebPagesDownloader.Tests

open NUnit.Framework
open FsUnit
open WebPagesDownloader
open System.Net

[<TestFixture>]
type WebPagesDownloaderTestsClass () =    
    [<Test>]
    member this.``Should not fail.`` () =
        // arrange
        let instance = WebPagesDownloader()

        // act
        instance.PrintSizeOfAllInnerWebPages "http://se.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova"
        |> Async.Start
        |> ignore

        // assert 
        ()                
        
    [<Test>]
    member this.``Should fail with incorrect url.`` () =
        let instance = WebPagesDownloader()
                        
        (fun () ->
            instance.PrintSizeOfAllInnerWebPages "http://se.math.spbasdu.ru/SE/2016/stipendiya-a.n.-terehova"            
            |> Async.RunSynchronously |> ignore) |> should throw typeof<System.Net.WebException>