namespace Test1.Tests

open FsUnit
open NUnit.Framework
open Stars

[<TestFixture>]
type StarsTestClass () =

    [<Test>]
    member this.``squreString with argument 1 shoule equal "*"``() =  
        (squreString 1).Value |> should equal "*\n"

    [<Test>]
    member this.``squreString with argument 0 shoule equal None``() =  
        squreString 0 |> should equal None

    [<Test>]
    member this.``squreString with negative argument shoule equal None``() =  
        squreString -5 |> should equal None

    [<Test>]
    member this.``squreString with argument 4 shoule equal ""``() =  
        (squreString 4).Value |> should equal "****\n*  *\n*  *\n****\n"


(*image: Visual Studio 2017 

before_build:
    - nuget restore Tests/Test1/Test1.sln

build:
    project: Tests/Test1/Test1.sln

test_script:
    - dotnet test Tests/Test1/Test1.sln*)
    