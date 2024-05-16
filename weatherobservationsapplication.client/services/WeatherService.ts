const GetWeather = async (location) =>
{
    //Add extensive error handling here
    let data;
    let errorMessage = "";
    try {

        let response = await fetch(`api/weatherforecast/${location}`);
        let responseData = await response.json();
        errorMessage = responseData.errorMessage;
        data = responseData.data;
        console.log(`Data in GetWeather: ${data}`);
    } catch (e) {
        console.log(`Error in Get Weather: ${e.Message}`)
    }

    return {data, errorMessage}
}


export { GetWeather }