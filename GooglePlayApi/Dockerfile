FROM alpine/git:latest as source
WORKDIR /src
RUN git clone https://github.com/facundoolano/google-play-api.git


FROM node:14 as final
WORKDIR /app
COPY --from=source /src/google-play-api /app
RUN npm install
CMD ["npm","start"]