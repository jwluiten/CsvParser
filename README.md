# CsvParser
A configurable CsvParser based on [FParsec](http://www.quanttec.com/fparsec/).

## deviations from rfc4180
With the following exceptions this implementation conforms to [rfc4180](https://tools.ietf.org/html/rfc4180)

* contrary to rfc4180 this parser allows for a variable number of columns per row
* rfc4180 specifies CRLF as the end of line sequence; this parser uses a platform dependent end of line
* the character for quoting fields is configurable; rfc4180 specifies the double quote character
* rfc4180 specifies comma as the field separation character; this parser allows specifying the field separator
* leading and trailing spaces are not considered part of an unquoted field, but are part of quoted fields
* characters in a field can have any value except those that serve as quote, separator or end of line, whereas rfc4180 restricts the possible characters in a field to %x20-21 / %x23-2B / %x2D-7E

## Comments welcome
This is my first adventure with FParsec. Coming from a Scala background I am used to parser combinators but this is my first attempt using FParsec. I welcome each and every comment on the code submitted.

## New to parser combinators?
Please start by reading the documentation on [FParsec](http://www.quanttec.com/fparsec/). For sample use cases please check the [tests](https://github.com/jwluiten/CsvParser/blob/master/Csv.test/Tests.fs) in this repository.
