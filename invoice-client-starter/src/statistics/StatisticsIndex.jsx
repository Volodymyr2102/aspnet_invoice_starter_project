import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api";

export default function StatisticsIndex() {
  const [stats, setStats] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    apiGet("/api/invoices/stats")
      .then((data) => setStats(data))
      .catch((err) => setError("Chyba při načítání statistik: " + err.message));
  }, []);

  if (error) return <div className="alert alert-danger">{error}</div>;
  if (!stats) return <div>Načítám statistiky...</div>;

  return (
    <div className="container mt-4">
      <h2> Statistiky faktur</h2>
      <hr />

      {/*  Celkový přehled */}
      <div className="card p-3 mb-4 shadow-sm">
        <h5>Souhrnné údaje</h5>
        <table className="table table-bordered mt-2">
          <tbody>
            <tr>
              <th>Počet faktur</th>
              <td>{stats.totalCount}</td>
            </tr>
            <tr>
              <th>Celková částka (bez DPH)</th>
              <td>{stats.totalPrice.toFixed(2)} Kč</td>
            </tr>
            <tr>
              <th>Celková částka (s DPH)</th>
              <td>{stats.totalWithVat.toFixed(2)} Kč</td>
            </tr>
            <tr>
              <th>Průměrné DPH</th>
              <td>{stats.averageVat.toFixed(2)} %</td>
            </tr>
          </tbody>
        </table>
      </div>

      {/*  Statistiky podle firem */}
      {stats.companyTurnovers && stats.companyTurnovers.length > 0 ? (
        <div className="card p-3 shadow-sm">
          <h5>Obrat podle firem (dodavatelů)</h5>
          <table className="table table-striped mt-2">
            <thead>
              <tr>
                <th>#</th>
                <th>ID dodavatele</th>
                <th>Počet faktur</th>
                <th>Celkový obrat (s DPH)</th>
              </tr>
            </thead>
            <tbody>
              {stats.companyTurnovers.map((c, index) => (
                <tr key={index}>
                  <td>{index + 1}</td>
                  <td>{c.sellerId}</td>
                  <td>{c.invoicesCount}</td>
                  <td>{c.totalTurnover.toFixed(2)} Kč</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <div className="alert alert-info mt-3">
          Žádné firemní statistiky nejsou k dispozici.
        </div>
      )}
    </div>
  );
}