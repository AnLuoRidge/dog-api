# Dog Api

The API project for dog client. By default, it runs on port 5000.

## Docker

- To deploy with Docker, please publish dog-api project first.
- Then open `/Dockerfile` and locate line 2 `COPY /release/ App/`.
- Replace `release` in with your publish folder.
- Build the image
- Run it with `-p 5000:80`

## Test

You could use the Test panel of Visual Studio to run all the test cases.

