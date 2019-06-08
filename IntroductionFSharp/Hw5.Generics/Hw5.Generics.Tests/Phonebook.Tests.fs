namespace Hw5.Generics.Tests

open FsCheck
open Phonebook
open NUnit.Framework
open FsUnit

[<TestFixture>]
type PhonebookTestClass () =
    


    [<Test>]
    member this.``Add record (name and phone)`` () =
        ()