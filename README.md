# Dog Api

The API project for dog client. By default, it runs on port 5000.

## Docker

- To deploy with Docker, please publish dog-api project first.
- Then open `/Dockerfile` and locate line 2 `COPY /release/ App/`.
- Replace `/release/` with your publish folder path.
- Build the image with `sudo docker build -t dog-api .`
- Run it with `sudo docker run -dp 5000:80 dog-api`

## Test

You could use the Test panel of Visual Studio to run all the test cases.

