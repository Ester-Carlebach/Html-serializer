# Html Serializer

##  Overview

**Html Serializer** is a lightweight and efficient tool written in **C#** for:

- **Parsing raw HTML** from web pages
- *Converting the HTML** into a structured C# object tree (DOM-like)
- **Querying the tree** using **CSS-like selectors**, enabling fast and intuitive access to elements

This is ideal for scraping, analyzing, or transforming HTML content in .NET applications.

---

##  Features

- Parse raw HTML into a hierarchical object model
- Navigate and search the structure using familiar **CSS selector syntax**
- Built-in support for common selectors (e.g. tag name, class, id, attributes)
- No external dependencies required (pure .NET)
- Fast and memory-efficient

---

##  How It Works

1. **HTML is parsed** into a tree of `HtmlNode` objects.
2. You can use the `.Query()` or `.Find()` methods with **CSS-like selectors** to locate elements.
3. Easily extract data, text content, or attributes from nodes.
## upported Selectors
.class
#id
tagname
[attribute=value]
Hierarchical selectors like div > span, ul li, etc.

##  Contributions
Pull requests and suggestions are welcome! Please open an issue for bugs or feature requests.
##  Contact
For questions or feedback:esty41655@gmail.com

---

##  Installation

Clone the repository or add the project to your solution. You can also convert it to a NuGet package if needed.
```bash
git clone https://github.com/Ester-Carlebach/html-serializer.git
