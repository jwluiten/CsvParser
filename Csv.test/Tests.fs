module Tests

  open System
  open Xunit
  open FsUnit

  open FParsec

  let csvParser = Csv.buildParser ',' '''   // Parser with comma as separator and single quote as quote character
  let tsvParser = Csv.buildParser '\t' '''  // Parser with tab as separator and  single quote as quote character

  let runParser parser input =
    let result = runParserOnString parser () "" input
    match result with
        | Success(result, _, _) -> result
        | Failure(errorMessage, _, _) -> failwith errorMessage

  [<Fact>]
  let ``Test csv-parser on single csv line`` () =
    let actual = runParser csvParser "1, 2 ,3"
    let expected = [["1";" 2 ";"3"]]
    actual |> should equal expected

  [<Fact>]
  let ``Test csv-parser on single csv line with quoted values`` () =
    let actual = runParser csvParser "' 1' ,2,' 3 '"
    let expected = [[" 1";"2";" 3 "]]
    actual |> should equal expected

  [<Fact>]
  let ``Test csv-parser on single csv line with quoted values using the quote in a field`` () =
    let actual = runParser csvParser "' 1',2,' 3'' '"
    let expected = [[" 1";"2";" 3' "]]
    actual |> should equal expected

  [<Fact>]
  let ``Test csv-parser on single csv line with quoted values using newline in a field`` () =
    let actual = runParser csvParser "' 1',2,' 3\nOn a newline '"
    let expected = [[" 1";"2";" 3\nOn a newline "]]
    actual |> should equal expected

  [<Fact>]
  let ``Test csv-parser on multiple lines`` () =
    let actual = runParser csvParser "1,2,3\n4,5,6"
    let expected = [["1";"2";"3"];["4";"5";"6"]]
    actual |> should equal expected
