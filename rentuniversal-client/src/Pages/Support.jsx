
import { useState } from "react";
import CityImage from "../assets/City.jpg";
import RentAllLogo from "../assets/logo.png";

// Defining the SupportPage functional component
export default function SupportPage() {
    // State variables for form inputs and submission status
    const [name, setName] = useState(""); // Stores the user's name
    const [email, setEmail] = useState(""); // Stores the user's email
    const [message, setMessage] = useState(""); // Stores the user's message
    const [status, setStatus] = useState(""); // Stores the status message after form submission

    // Handles form submission
    async function handleSubmit(e) {
        e.preventDefault(); // Prevents the default form submission behavior
        setStatus(""); // Resets the status message

        try {
            
            // Resets form fields and sets a success message
            setName("");
            setEmail("");
            setMessage("");
            setStatus("Tak for din besked. Vi vender tilbage hurtigst muligt.");
        } catch (err) {
            // Logs the error and sets an error message
            console.error(err);
            setStatus("Der skete en fejl. Prøv igen.");
        }
    }

    return (
        <div style={{ padding: "20px" }}>
            {/* OM OS + CITY-BILLEDE SIDE-OM-SIDE */}
            <section className="about-wrapper">
                {/* Venstre side: tekst */}
                <div className="about-info">
                    <h2>Om os</h2>

                    <p>
                        RentAll er en udlejningsplatform udviklet af et team p&aring; fire
                        softwareudviklere. Platformen samler forskelligt udstyr ét sted –
                        fra værktøj og udendørsudstyr til festartikler – s&aring; brugerne nemt
                        kan finde og reservere det, de har brug for.
                    </p>

                    <p>
                        Idéen bag løsningen er, at du kun betaler for det udstyr, du
                        anvender, og kun i den periode, hvor du faktisk har behov for det.
                        P&aring; den m&aring;de kan b&aring;de private og mindre virksomheder undg&aring;
                        unødvendige køb og i stedet leje fleksibelt efter behov.
                    </p>

                    {/* Firmaoplysninger */}
                    <p><strong>Navn:</strong> RentAll ApS</p>
                    <p><strong>Telefon:</strong> +45 12 34 56 78</p>
                    <p><strong>Email:</strong> support@rentall.dk</p>
                    <p><strong>Lokation:</strong> Sønderborg, Danmark</p>
                </div>

                {/* Højre side: city-billede */}
                <div className="about-image">
                    <img src={CityImage} alt="Moderne by" />
                </div>
            </section>

            {/* KONTAKT-BOKS UNDER OM-OS */}
            <section className="contact-box">
                <div className="support-header">
                    <h2>Kontakt os</h2>
                    <img
                        src={RentAllLogo}
                        alt="RentAll logo"
                        className="support-logo"
                    />
                </div>

                <p className="contact-text">
                    Har du spørgsm&aring;l eller efterspørgsel til udlejning? Skriv en besked her:
                </p>

                {/* Kontaktformular */}
                <form className="contact-form" onSubmit={handleSubmit}>
                    {/* Navn inputfelt */}
                    <label>
                        Navn
                        <input
                            type="text"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </label>

                    {/* Email inputfelt */}
                    <label>
                        Email
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </label>

                    {/* Besked inputfelt */}
                    <label>
                        Besked
                        <textarea
                            rows={5}
                            value={message}
                            onChange={(e) => setMessage(e.target.value)}
                            required
                        />
                    </label>

                    {/* Send knap */}
                    <button type="submit" className="primary-btn contact-btn">
                        Send besked
                    </button>

                    {/* Statusbesked */}
                    {status && <p className="contact-status">{status}</p>}
                </form>
            </section>
        </div>
    );
}
