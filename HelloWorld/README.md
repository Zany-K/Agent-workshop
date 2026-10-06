# Hello world!
An introduction to setting up a local LLM and interacting with it.

## Prerequisite
* Download Ollama
  * Mac: ``brew install ollama`` or ``curl -fsSL https://ollama.com/install.sh | sh``
  * Windows: ``https://ollama.com/download/windows``
* Download an open weight model that fits on your machine and supports agentic work.
  * Qwen3-9b model works great for mac m4 24gb
  * Qwen2.5:7b model works for almost all machines and can perform agentic tasks

## Setup
### Required Nugets
* ``dotnet add package OllamaSharp``
* ``dotnet add package Microsoft.Extensions.AI``
