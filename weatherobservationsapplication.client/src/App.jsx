import { useEffect, useState } from 'react';
import './App.css';
import { GetWeather } from '../services/WeatherService'

function App() {

    const [location, setLocation] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState();
    const [error, setError] = useState(false);
    const [weatherData, setWeatherData] = useState(new Array());

    //Maintain updated state for user input
    const handleChange = (e) => {
        setLocation(e.target.value);
    }

    //Handle submission of the form
    const handleSubmit = async (e) => {
        e.preventDefault();     //Prevent page refresh

        setIsLoading(true);

        //Reset values from previous call
        setIsError(false);
        setError(false);
        setWeatherData(new Array());

        //Check if any locationw as provided
        if (!location) {
            setError("Please specify a location to search for.")
            setIsLoading(false);
            return;
        }

        //If this were C#, I would also add ways in which the user can cancel the request and
        //handling for when a user attemps to press the button multiple times.
        //As I'm just getting back to working with Javascript, I still need a refresher on the syntax
        //for handling this in Javascript
        try {
            console.log(`Attempting to grab weather for ${location}`)
            var res = await GetWeather(location);
            console.log(`Returning from GetWeather. Error ${res.errorMessage}, data: ${res.data}`)
            setWeatherData(res.data);
            setError(res.errorMessage);
        } catch (e) {
            setError(e.errorMessage);
        } finally {
            setIsLoading(false);
        }


    }



    return (
        <>
            <h1>Weather Observations</h1>
            <div>
                <form onSubmit={handleSubmit}>
                    <div className='form-element'>
                        <label className='form-element-spacing' htmlFor='location'>Please enter a location:</label>
                        <input className='form-element-spacing' type='text' id='location' autoFocus name='location' onChange={handleChange} />
                        <button type='submit'>Search Location</button>
                    </div>
                </form>
            </div>
            <div>
                {isLoading && <h1>Loading....</h1>}
                {error && (
                    <div>
                        <h3>Error encountered. Please try again.</h3>
                        <p>Error Message: {error}</p>
                    </div>
                )}
                {weatherData !== undefined && weatherData !== null && weatherData.length != 0 ? (
                <table>
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>High Temperature</th>
                            <th>Low Temperature</th>
                        </tr>
                    </thead>
                    <tbody>
                        {weatherData.Map(data =>
                            <tr key={data.Date}>
                                <td>{data.Date}</td>
                                <td>{data.HighTemperature}</td>
                                <td>{data.LowTemperature}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                ) : <></>}


            </div>



        </>
    )
}

export default App;