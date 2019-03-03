FROM mono:latest
WORKDIR /app

# copy and build everything else
COPY . ./
RUN mcs GreetingApp/Greetings.cs
CMD ["mono", "GreetingApp/Greetings.exe", "Creator"]