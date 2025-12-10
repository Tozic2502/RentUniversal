// src/Pages/Page-Support/Support.jsx
import { useState } from "react";
import CityImage from "../../assets/City.jpg";
import RentAllLogo from "../../assets/logo.png";

export default function SupportPage() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [message, setMessage] = useState("");
    const [status, setStatus] = useState("");

    async function handleSubmit(e) {
        e.preventDefault();
        setStatus("");

        try {
            const response = await fetch("http://localhost:8080/api/support-messages", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ name, email, message }),
            });

            if (!response.ok) throw new Error("Request failed");

            setName("");
            setEmail("");
            setMessage("");
            setStatus("Tak for din besked. Vi vender tilbage hurtigst muligt.");
        } catch (err) {
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
                        RentAll er en udlejningsplatform udviklet af et team på fire
                        softwareudviklere. Platformen samler forskelligt udstyr ét sted –
                        fra værktøj og udendørsudstyr til festartikler – så brugerne nemt
                        kan finde og reservere det, de har brug for.
                    </p>

                    <p>
                        Idéen bag løsningen er, at du kun betaler for det udstyr, du
                        anvender, og kun i den periode, hvor du faktisk har behov for det.
                        På den måde kan både private og mindre virksomheder undgå
                        unødvendige køb og i stedet leje fleksibelt efter behov.
                    </p>

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
                    Har du spørgsmål eller efterspørgsel til udlejning? Skriv en besked her:
                </p>

                <form className="contact-form" onSubmit={handleSubmit}>
                    <label>
                        Navn
                        <input
                            type="text"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </label>

                    <label>
                        Email
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </label>

                    <label>
                        Besked
                        <textarea
                            rows={5}
                            value={message}
                            onChange={(e) => setMessage(e.target.value)}
                            required
                        />
                    </label>

                    <button type="submit" className="primary-btn contact-btn">
                        Send besked
                    </button>

                    {status && <p className="contact-status">{status}</p>}
                </form>
            </section>
        </div>
    );
}
