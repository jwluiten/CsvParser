module Csv

open FParsec

// csvData ::= (csvRecord)* 'EOF'
// csvRecord ::= ws* csvFiedlList ('\n' | 'EOF')
// csvFieldList ::= csvField ws* (sep ws* csvFieldList)*
// csvField ::= rawField | quotedString
// rawField ::= (any character except '\n' 'EOF' sep)*
// quotedField :== quote (quote quote | char)* quote
// ws :== (any character in " \t" except sep)*

// value restriction (see FParsec documentation)
type P<'t> = Parser<'t, Unit>

// general helpers
let debug (p: P<_>) stream =
    p stream // set a breakpoint here

let first (s: string) = s.Substring(0,1)
let trim (s: string) = s.Trim()

let doublechar (c: char):P<_> =
  let ds = [c;c] |> System.String.Concat
  (pstring ds) |>> first

let quotedstring (quote: char):P<_> =
  let qstring = string quote
  let doublequote = doublechar quote
  let normalCharSnippet = manySatisfy (fun c -> c <> quote)
  between (pstring qstring) (pstring qstring)
    (stringsSepBy normalCharSnippet doublequote)

// given a separator character and a quote character, return a parser
let buildParser (sep: char) (quote: char): P<_> =
  let ws = if '\t' = sep then " " else " \t"
  let skipWs = many (skipAnyOf ws)
  let fieldStopSet = "\n" + string sep
  let rawString = many (noneOf fieldStopSet) |>> System.String.Concat
  let qstring = (quotedstring quote)
  let csvField = (qstring .>> skipWs) <|> rawString
  let csvRecord = sepBy csvField (skipChar sep)
  manyTill (csvRecord .>> (skipNewline <|> eof)) eof
